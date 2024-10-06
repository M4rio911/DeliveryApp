using DeliveryApp.API.Configuration;
using DeliveryApp.Application;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
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

//IDENTITY???
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DeliveryDbContext>()
    .AddDefaultTokenProviders();

//JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]);
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
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("https://localhost:8081/")

            .SetIsOriginAllowed(origin => true)
            .AllowCredentials()
            .WithExposedHeaders("Access-Control-Allow-Origin", "Access-Control-Allow-Methods");
    });
});

//SWAGGER
builder.Services.AddSwaggerGen();

//MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationReferenceAssembly).Assembly));

//DB
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<DeliveryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DeliveryDBConnection"));
});

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

app.UseAuthentication();
app.UseAuthorization();

//app.UseCors(builder => builder
//    .SetIsOriginAllowed(origin => true)
//    .AllowAnyMethod()
//    .AllowAnyHeader()
//    .AllowCredentials()
//    .WithExposedHeaders("Access-Control-Allow-Origin", "Access-Control-Allow-Methods"));

app.UseCors("AllowAll");

app.MapControllers();

app.Run();