using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Integration.Repositories.DBConfiguration
{
    public class DataOptionFactory
    {
        public string DefaultConnection { get; set; }
        public IDbConnection DatabaseConnection => new SqlConnection(DefaultConnection);
    }
}
