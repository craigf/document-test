using System.Net.Http.Headers;
using System.Text;
using DocumentManagement.API;
using DocumentManagement.Infrastructure;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Xunit;

namespace DocumentManagement.Tests.Integration.Helpers
{
    public class ClaimInjectorApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private const string Database = "document";
        private const string Username = "sa";
        private const string Password = "Password123!";
        private const ushort MsSqlPort = 1433;

        private readonly IContainer _mssqlContainer;

        /// <summary>
        /// The main customization point of the claims.
        /// </summary>
        public ClaimInjectorHandlerHeaderConfigHelper RoleConfigHelper { get; } = new();

        public TestContextConfiguration TestContextConfiguration { get; private set; }

        public ClaimInjectorApplicationFactory()
        {
            _mssqlContainer = new ContainerBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPortBinding(MsSqlPort, true)
                .WithEnvironment("ACCEPT_EULA", "Y")
                .WithEnvironment("SQLCMDUSER", Username)
                .WithEnvironment("SQLCMDPASSWORD", Password)
                .WithEnvironment("MSSQL_SA_PASSWORD", Password)
                .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(MsSqlPort))
                .Build();
        }
        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);

            if (RoleConfigHelper.AnonymousRequest)
            {
                return;
            }

            var json = JsonConvert.SerializeObject(RoleConfigHelper);

            var base64Json = Convert.ToBase64String(Encoding.UTF8.GetBytes(json));

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bypass", base64Json);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            TestContextConfiguration = new TestContextConfiguration($"Server={_mssqlContainer.Hostname},{_mssqlContainer.GetMappedPublicPort(MsSqlPort)};Database={Database};User Id={Username};Password={Password};TrustServerCertificate=True");


            builder.UseEnvironment("test");
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<DocumentContext>));

                services.Replace(new ServiceDescriptor(typeof(IContextConfiguration), TestContextConfiguration));

                services.AddAuthentication(x =>
                    {
                        x.DefaultScheme = ClaimInjectorHandler.AuthenticationScheme;
                        x.DefaultAuthenticateScheme = ClaimInjectorHandler.AuthenticationScheme;
                        x.DefaultChallengeScheme = ClaimInjectorHandler.AuthenticationScheme;
                    })
                    .AddScheme<ClaimInjectorHandlerOptions, ClaimInjectorHandler>(
                        ClaimInjectorHandler.AuthenticationScheme,
                        x => { });
            });

            base.ConfigureWebHost(builder);
        }



        public async Task InitializeAsync()
        {
            await _mssqlContainer.StartAsync();

            using var scope = Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DocumentContext>();
            await dbContext.Database.MigrateAsync();
        }

        public new async Task DisposeAsync()
        {
            await _mssqlContainer.DisposeAsync();
        }
    }
}
