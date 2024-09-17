using DeliveryApp.API.Configuration;
using DeliveryApp.Persistance;
using Microsoft.EntityFrameworkCore;
using Serilog;

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

//SWAGGER
builder.Services.AddSwaggerGen();

//DB
builder.Services.AddDbContext<DeliveryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

////ENTITY
//builder.Services.AddIdentity<User, Role>()
//    .AddEntityFrameworkStores<DeliveryDbContext>()
//    .AddUserManager<UserManager<User>>()
//    .AddRoleManager<UserManager<Role>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();