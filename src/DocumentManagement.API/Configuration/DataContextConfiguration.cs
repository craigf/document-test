using DocumentManagement.Infrastructure;

namespace DocumentManagement.API.Configuration
{
    public class DataContextConfiguration : IContextConfiguration
    {
        public DataContextConfiguration(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("DefaultConnection");
        }

        public string ConnectionString { get; }
    }
}
