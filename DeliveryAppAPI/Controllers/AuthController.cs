using DeliveryApp.Application.Models;
using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeliveryApp.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var userByName = await _userManager.FindByNameAsync(model.UsernameOrEmail);
        var userEmail = await _userManager.FindByEmailAsync(model.UsernameOrEmail);
        var user = userByName ?? userEmail ?? null;

        if (user != null 
            && user.ActiveStatus == true
            && await _userManager.CheckPasswordAsync(user, model.Password))
        {

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
        }

        return Unauthorized("Invalid credentials.");
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> Login([FromBody] ResetPasswordModel model)
    {
        var userName = User.Identity?.Name;
        var loggedUser = await _userManager.FindByNameAsync(userName);

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        var isAdmin = await _userManager.IsInRoleAsync(loggedUser, "Admin");
        var isSupport = await _userManager.IsInRoleAsync(loggedUser, "Support");

        if (user.Id != model.UserId && !isAdmin && !isSupport)
        {
            return BadRequest("User not found.");
        }

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetResult = await _userManager.ResetPasswordAsync(user, resetToken, model.NewPassword);

        if (resetResult.Succeeded)
        {
            return Ok("New password was set");
        }

        foreach (var error in resetResult.Errors)
        {
            Console.WriteLine($"Error: {error.Description}");
        }

        return BadRequest();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userName = !string.IsNullOrEmpty(model.Username) ? model.Username: model.Email;

        var user = new User() { 
            UserName = userName, 
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserTypeId = 1,
            ActiveStatus = true
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }
        await _userManager.AddToRoleAsync(user, "Client");

        return Ok();
    }

    [HttpGet("me"), Authorize]
    public async Task<IActionResult> GetUserData()
    {
        var userName = User.Identity?.Name;

        if (string.IsNullOrEmpty(userName))
        {
            return Unauthorized("Nie można odnaleźć zalogowanego użytkownika.");
        }

        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return NotFound("Nie znaleziono użytkownika.");
        }

        var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

        return Ok(new
        {
            user.Id,
            user.UserName,
            user.Email,
            userRole
        });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, _userManager.GetRolesAsync(user).Result.FirstOrDefault() ?? "")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
