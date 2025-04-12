using DeliveryApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DeliveryApp.API.Configuration;

public static class InitRoles
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roleNames = { "Client", "Delivery", "DeliveryManager", "Support", "Admin" };

        foreach (var roleName in roleNames)
        {
            var roleExists = await roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
    public static async Task SeedAdminUserAsync(UserManager<User> userManager)
    {
        string adminEmail = "admin@gmail.com";
        string login = "admin";
        string adminPassword = "zaq1@WSX";

        var existingAdmin = await userManager.FindByEmailAsync(adminEmail);
        if (existingAdmin != null)
        {
            if (!await userManager.IsInRoleAsync(existingAdmin, "Admin"))
            {
                await userManager.AddToRoleAsync(existingAdmin, "Admin");
            }
            return;
        }

        var adminUser = new User
        {
            UserName = login,
            FirstName = "admin",
            LastName = "admin",
            UserTypeId = 4,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
        else
        {
            throw new Exception("Failed to create default admin: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }
}
