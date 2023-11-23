using DocumentManagement.Tests.Integration.Helpers;
using Xunit;

namespace DocumentManagement.Tests.Integration
{
    public abstract class ControllerTestBase : IClassFixture<ClaimInjectorApplicationFactory>
    {
        public readonly ClaimInjectorApplicationFactory Factory;

        public HttpClient Client { get; private set; }

        protected ControllerTestBase(ClaimInjectorApplicationFactory factory)
        {
            Factory = factory;
            Factory.RoleConfigHelper.Reset();
            Client = Factory.CreateClient();
        }
    }
}
