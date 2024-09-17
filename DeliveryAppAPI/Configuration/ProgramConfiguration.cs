namespace DeliveryApp.API.Configuration;

public static class ProgramConfiguration
{
    public static void LoadAppConfiguration(this WebApplicationBuilder builder)
    {
        var env = builder.Environment;
        var rootPath = env.ContentRootPath;
        var commonFolder = Path.Combine(rootPath, "config");
        var path = Path.Combine(commonFolder, "appsettings.json");

        builder.Configuration
            .AddJsonFile(path, optional: true, reloadOnChange: false)
            .AddJsonFile("/appsettings.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }
}
