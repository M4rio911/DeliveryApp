using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace DeliveryApp.API.Configuration;

public static class SwaggerExtensionsConfiguration
{
    /// <summary>
    ///     Adds the swagger documentation.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>IServiceCollection.</returns>
    //public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    //{
    //    services.AddSwaggerGen(swaggerOptions =>
    //    {
    //        swaggerOptions.SwaggerDoc("v1", new OpenApiInfo
    //        {
    //            Title = "DeliveryApp.API.Configuration",
    //            Version = "v1",
    //            Description = "DeliveryApp API Configuration",
    //        });
    //        //swaggerOptions.SchemaFilter<EnumSchemaFilter>();
    //        swaggerOptions.OrderActionsBy(x => x.RelativePath);
    //        var basePath = AppContext.BaseDirectory;
    //        foreach (var name in Directory.GetFiles(basePath, "*.XML", SearchOption.AllDirectories))
    //        {
    //            swaggerOptions.IncludeXmlComments(name);
    //        }

    //        var securityScheme = new OpenApiSecurityScheme()
    //        {
    //            Name = "Authorization",
    //            Description = "Enter JWT Bearer token",
    //            Type = SecuritySchemeType.Http,
    //            Scheme = "bearer",
    //            BearerFormat = "JWT",
    //            In = ParameterLocation.Header,
    //            Reference = new OpenApiReference
    //            {
    //                Id = JwtBearerDefaults.AuthenticationScheme,
    //                Type = ReferenceType.SecurityScheme
    //            },
    //            Flows = new OpenApiOAuthFlows
    //            {
    //                ClientCredentials = new OpenApiOAuthFlow
    //                {
    //                    AuthorizationUrl = new Uri("/connect/token", UriKind.Relative),
    //                    TokenUrl = new Uri("/connect/token", UriKind.Relative)
    //                }
    //            },
    //            OpenIdConnectUrl = new Uri("/.well-known/openid-configuration", UriKind.Relative)
    //        };

    //        swaggerOptions.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    //        //swaggerOptions.SchemaFilter<NullablePropertiesSchemaFilter>();
    //        swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
    //        {
    //            {securityScheme, new string[] { }}
    //        });
    //    });

    //    return services;
    //}
    //internal static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app,
    //       IConfiguration configuration)
    //{
    //    var swaggerOptions = configuration.GetSection("Swagger").Get<SwaggerApiConfiguration>();
    //    if (swaggerOptions != null && swaggerOptions.SwaggerEnabled)
    //    {
    //        app.UseSwagger();
    //        app.UseSwaggerUI(c =>
    //        {
    //            c.SwaggerEndpoint(swaggerOptions.Url, swaggerOptions.Name);
    //            if (!string.IsNullOrEmpty(swaggerOptions.RoutePrefix)) c.RoutePrefix = swaggerOptions.RoutePrefix;

    //            c.DocExpansion(DocExpansion.List);
    //        });
    //    }

    //    return app;
    //}
}
