namespace DeliveryApp.API.Configuration;

public static class SwaggerApiConfiguration
{
    //public static WebApplicationBuilder RegisterApiConfigurations(this WebApplicationBuilder builder)
    //{
    //    var swaggerOptions = builder.Configuration.GetSection("Swagger").Get<SwaggerConfiguration>();

    //    if (swaggerOptions != null && swaggerOptions.SwaggerEnabled)
    //    {
    //        builder.Services.AddSwaggerDocumentation();
    //    }

    //    return builder;
    //}
}

public class SwaggerConfiguration
{
    /// <summary>
    ///     Gets or sets a value indicating whether [swagger enabled].
    /// </summary>
    /// <value><c>true</c> if [swagger enabled]; otherwise, <c>false</c>.</value>
    public bool SwaggerEnabled { get; set; }

    /// <summary>
    ///     Gets or sets the URL.
    /// </summary>
    /// <value>The URL.</value>
    public string Url { get; set; } = "/swagger/v1/swagger.json";

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = "Smyk.Crm.Services.Crm";

    /// <summary>
    /// Gets or sets the route prefix.
    /// </summary>
    /// <value>The route prefix.</value>
    public string? RoutePrefix { get; set; }
}