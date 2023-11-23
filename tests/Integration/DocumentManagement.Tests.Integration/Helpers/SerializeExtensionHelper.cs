using System.Text;
using Newtonsoft.Json;

namespace DocumentManagement.Tests.Integration.Helpers
{
    public static class SerializeExtensionHelper
    {
        public static StringContent SerializeRequestObject(object value)
        {
            return new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
        }
    }
}
