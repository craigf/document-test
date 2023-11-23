using DocumentManagement.Core.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace DocumentManagement.API.Configuration
{
    public static class ApplicationConfiguration
    {
        public static void RegisterApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureExternalIdp(services, configuration);
            services.Configure<KeyCloakSetting>(configuration.GetSection("KeyCloak"));
        }

        private static void ConfigureExternalIdp(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = configuration["KeyCloak:ServerAddress"] +
                                        "/auth/realms/" + configuration["KeyCloak:Realm"];
                    options.Audience = configuration["KeyCloak:Audience"];
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Jwt:Authority"],
                        ValidateLifetime = true,
                        RequireExpirationTime = true
                    };
                });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrators", policy => policy.RequireClaim("user_realm_roles", "[Administrators]"));
            });
        }

        public static void RegisterApplicationServices(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
