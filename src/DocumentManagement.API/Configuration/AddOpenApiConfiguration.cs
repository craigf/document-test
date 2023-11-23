using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using DocumentManagement.Core.Settings;

namespace DocumentManagement.API.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class OpenApiConfiguration
    {
        /// <summary>
        /// Configuration for Identity provider authentication
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddOpenApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var keyCloak = configuration.GetSection("KeyCloak").Get<KeyCloakSetting>();

            services.AddSwaggerGen(options =>
            {
                var scheme = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl =
                                new Uri(
                                    $"{keyCloak.ServerAddress}/auth/realms/{keyCloak.Realm}/protocol/openid-connect/auth"),
                            TokenUrl = new Uri(
                                $"{keyCloak.ServerAddress}/auth/realms/{keyCloak.Realm}/protocol/openid-connect/token")
                        }
                    },
                    Type = SecuritySchemeType.OAuth2
                };

                options.AddSecurityDefinition("OAuth", scheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Id = "OAuth", Type = ReferenceType.SecurityScheme }
                        },
                        new List<string>
                        {
                            Capacity = 0
                        }
                    }
                });

                options.DocInclusionPredicate((version, apiDescription) =>
                {
                    var values = apiDescription.RelativePath
                        .Split('/')
                        .Select(v => v.Replace("v{version}", version));

                    apiDescription.RelativePath = string.Join("/", values);

                    var versionParameter = apiDescription.ParameterDescriptions
                        .SingleOrDefault(p => p.Name == "version");

                    if (versionParameter != null)
                        apiDescription.ParameterDescriptions.Remove(versionParameter);

                    return true;
                });

                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Document API", Version = "v1" });
            });
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="configuration"></param>
        public static void AddOpenApiConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            var keyCloak = configuration.GetSection("KeyCloak").Get<KeyCloakSetting>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.OAuthClientId(keyCloak.Audience);
                options.OAuthScopes("profile", "openid", "email");
                options.OAuthUsePkce();
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Document API" + " v1");
            });
        }
    }
}
