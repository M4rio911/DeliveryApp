namespace DeliveryApp.API.Configuration;

public static class CorsConfiguration
{
    public static void ConfigureCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy
                .WithOrigins("http://localhost:8080")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
            });
        });
    }
}
