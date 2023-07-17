using Infrastructure.DBConfiguration.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Integration.Repositories.DBConfiguration.EFCore
{
    public class EntityFrameworkConnection
    {
        private IServiceProvider _serviceProvider;

        public ApplicationContext DataBaseConfiguration()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(DatabaseConnection.ConnectionConfiguration.Value.DefaultConnection));
            _serviceProvider = services.BuildServiceProvider();
            return _serviceProvider.GetService<ApplicationContext>()!;
        }
    }
}
