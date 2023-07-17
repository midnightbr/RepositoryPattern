using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Integration.Repositories.DBConfiguration
{
    public class DatabaseConnection
    {
        public static IOptions<DataOptionFactory> ConnectionConfiguration
        {
            get
            {
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.test.json").Build();

                return Options.Create(configuration.GetSection("ConnectionStrings").Get<DataOptionFactory>());
            }
        }
    }
}
