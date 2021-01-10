using Microsoft.Extensions.Configuration;

namespace Bit8.Students.Common
{
    public class BConfiguration : IBConfiguration
    {
        public string ConnectionString { get; set; }

        public BConfiguration(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("ConnectionString");
        }        
    }
}