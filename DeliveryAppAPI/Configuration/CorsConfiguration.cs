namespace DeliveryApp.API.Configuration;

public static class CorsConfiguration
{
    public static void ConfigureCors(this WebApplicationBuilder builder)
    {
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
    }
}
