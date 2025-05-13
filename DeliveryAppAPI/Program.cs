using DeliveryApp.API.Configuration;
using DeliveryApp.Application;
using DeliveryApp.Application.Interfaces.Repositories;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Infrastructure.Repositories;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.LoadAppConfiguration();

var configuration = builder.Configuration;

//SERILOG
builder.ConfigureSerilog();
builder.Host.UseSerilog();

//APP
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddScoped<IDictionaryRepository, DictionaryRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

//CORS
builder.ConfigureCors();

//SWAGGER
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

//MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationReferenceAssembly).Assembly));

//DB
builder.Services.AddDbContext<DeliveryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DeliveryDBConnection"));
});

//Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<DeliveryDbContext>()
    .AddUserManager<UserManager<User>>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
        ValidateIssuerSigningKey = true
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.Request.Headers["Authorization"].ToString();
                if (token.StartsWith("Bearer "))
                    context.Token = token.Substring("Bearer ".Length).Trim();
            }

            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// MIGRATIONS + ROLES
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DeliveryDbContext>();
    var retries = 5;
    while (retries > 0)
    {
        try
        {
            dbContext.Database.Migrate();
            Console.WriteLine("DB CONNECTION STRING: " + builder.Configuration.GetConnectionString("DeliveryDBConnection"));

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<User>>();


            //SEED VALUES - only 1st run
            //await InitRoles.SeedDictionaries(dbContext);
            //await InitRoles.SeedRolesAsync(roleManager);
            //await InitRoles.SeedAdminUserAsync(userManager);

            break;
        }
        catch (Exception ex)
        {
            retries--;
            Console.WriteLine($"[Startup Retry] DB not ready: {ex.Message}");
            if (retries == 0)
                throw;

            await Task.Delay(5000);
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();