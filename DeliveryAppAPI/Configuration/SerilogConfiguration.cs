using Serilog;
using ILogger = Serilog.ILogger;

namespace DeliveryApp.API.Configuration;

public static class SerilogConfiguration
{
    public static WebApplicationBuilder ConfigureSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = CreateLogger(builder.Configuration);

        builder.Services.AddSingleton<ILogger>(Log.Logger);

        return builder;
    }
    private static ILogger CreateLogger(IConfiguration configuration)
    {
        var loggerConfiguration = new LoggerConfiguration().ReadFrom.Configuration(configuration);

        var logger = loggerConfiguration
            .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
            .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
            .Enrich.WithMachineName()
            .Enrich.FromLogContext()
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger") || p.Value.ToString().Contains("health")))
            .WriteTo.Console()
            .CreateLogger();
        return logger;
    }
}
