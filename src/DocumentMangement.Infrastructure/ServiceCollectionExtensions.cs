using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using DocumentManagement.Domain.Documents;
using DocumentManagement.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;

namespace DocumentManagement.Infrastructure
{

    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DocumentContext>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();
            return services;
        }
    }
}
