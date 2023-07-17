using Infrastructure.Interfaces.DBConfiguration;
using Infrastructure.Interfaces.Repositories.Domain;
using NUnit.Framework;
using NUnitTest.Integration.Repositories.DBConfiguration.Dapper;
using NUnitTest.Integration.Repositories.Repositories.DataBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Integration.Repositories.Repositories.Dapper
{
    [TestFixture]
    public class UserRepositoryTransactionTest
    {
        private IDatabaseFactory _databaseFactory;
        private IDbTransaction _transaction;

        private IUserRepository _userRepository;
        private ITaskToDoRepository _taskToDoRepository;
        private UserBuilder _userBuilder;
        private TaskToDoBuilder _taskToDoBuilder;

        [OneTimeSetUp]
        public void GlobalPrepare()
        {
            _databaseFactory = new DapperConnection().DatabaseFactory();
        }

        [SetUp]
        public void Inicializar()
        {

        }
    }
}
