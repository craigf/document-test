using DocumentManagement.Infrastructure;

namespace DocumentManagement.Tests.Integration.Helpers
{
    public class TestContextConfiguration : IContextConfiguration
    {
        public TestContextConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}
