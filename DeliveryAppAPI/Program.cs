using DeliveryApp.API.Configuration;
using DeliveryApp.Application;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;

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

//JWT
//var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]);
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.SaveToken = true;
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(key),
//        ValidateIssuer = false,
//        ValidateAudience = false
//    };
//});

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
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DeliveryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DeliveryDBConnection"));
});

//IDENTITY???
//builder.Services.AddIdentity<User, IdentityRole>()
//    .AddEntityFrameworkStores<DeliveryDbContext>()
//    .AddDefaultTokenProviders();

//builder.Services.AddIdentity<User, IdentityRole>(options =>
//{
//    options.User.RequireUniqueEmail = true;
//    options.SignIn.RequireConfirmedEmail = false;
//})
//    .AddEntityFrameworkStores<DeliveryDbContext>()
//    .AddDefaultTokenProviders();

builder.Services.AddIdentityApiEndpoints<User>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
})
    .AddEntityFrameworkStores<DeliveryDbContext>()
    .AddDefaultTokenProviders();

////ENTITY
//builder.Services.AddIdentity<User, Role>()
//    .AddEntityFrameworkStores<DeliveryDbContext>()
//    .AddUserManager<UserManager<User>>()
//    .AddRoleManager<UserManager<Role>>();

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<User>();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");
app.MapControllers();

app.Run();