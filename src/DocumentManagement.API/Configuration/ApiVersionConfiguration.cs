using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace DocumentManagement.API.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class ApiVersionConfiguration
    {
        public static void AddApiVersionConfiguration<T>(this IServiceCollection services) where T : ControllerBase
        {
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new MediaTypeApiVersionReader("ver"));
                o.Conventions.Controller<T>().HasApiVersion(new ApiVersion(1, 0));
            });
        }
    }
}
