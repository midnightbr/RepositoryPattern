using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.DBConfiguration
{
    public class IDatabaseFactory
    {
        IDbConnection GetDbConnection { get; }
    }
}
