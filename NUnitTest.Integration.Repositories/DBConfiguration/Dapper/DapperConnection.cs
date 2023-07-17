using Infrastructure.DBConfiguration.Dapper;
using Infrastructure.Interfaces.DBConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Integration.Repositories.DBConfiguration.Dapper
{
    public class DapperConnection
    {
        private IServiceProvider _provider;
        public IOptions<DataSettings> DataSettings()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddTransient(provider => Options.Create(
                new DataSettings { 
                    DefaultConnection = DatabaseConnection.ConnectionConfiguration.Value.DefaultConnection 
                }));
            _provider = services.BuildServiceProvider();

            return _provider.GetService<IOptions<DataSettings>>();
        }

        public IDatabaseFactory DatabaseFactory()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddTransient<IDatabaseFactory, DatabaseFactory>(_ => new DatabaseFactory(this.DataSettings()));
            _provider = services.BuildServiceProvider();

            return _provider.GetService<IDatabaseFactory>();
        }
    }
}
