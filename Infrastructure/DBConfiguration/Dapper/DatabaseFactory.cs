using Infrastructure.Interfaces.DBConfiguration;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DBConfiguration.Dapper
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private IOptions<DataSettings> _dataSettings;
        protected string ConnectionString => !string.IsNullOrEmpty(_dataSettings.Value.DefaultConnection)
            ? _dataSettings.Value.DefaultConnection
            : DataBaseConnection.Configuration.GetConnectionString("DefaultConnection");

        public IDbConnection GetDbConnection => new SqlConnection(ConnectionString);
        public DatabaseFactory(IOptions<DataSettings> dataSettings)
        {
            _dataSettings = dataSettings;
        }
    }
}
