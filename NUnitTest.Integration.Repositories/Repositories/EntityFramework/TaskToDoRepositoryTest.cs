using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Domain.EFCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using NUnitTest.Integration.Repositories.DBConfiguration.EFCore;
using NUnitTest.Integration.Repositories.Repositories.DataBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Integration.Repositories.Repositories.EntityFramework
{
    [TestFixture]
    public class TaskToDoRepositoryTest
    {
        private ApplicationContext _dbContext;
        private IDbContextTransaction _dbTransaction;

        private IUserRepository _userRepository;
        private ITaskToDoRepository _taskToDoRepository;
        private UserBuilder _userBuilder;

        [OneTimeSetUp]
        public void GlobalPrepare()
        {
            _dbContext = new EntityFrameworkConnection().DataBaseConfiguration();
        }

        [SetUp]
        public void Inicializa()
        {
            _userRepository = new UserRepository(_dbContext);
            _taskToDoRepository = new TaskToDoRepository(_dbContext);
            _userBuilder = new();
            _dbTransaction = _dbContext.Database.BeginTransaction();
        }

        [TearDown]
        public void ExecutadoAposExecucaoDeCadaTeste()
        {
            _dbTransaction.Rollback();
        }

        [Test]
        public async Task GetAllIncludingUserAsync()
        {
            var user = await _userRepository.AddAsync(_userBuilder.CreateUserWithTasks(2));
            var tasks = user.TaskToDo;
            var result = await _taskToDoRepository.GetAllUserAsync();

            Assert.AreEqual(result.FirstOrDefault().UserId, user.Id);
            Assert.AreEqual(result.LastOrDefault().UserId, user.Id);
        }

        [Test]
        public async Task GetByIncludingUserAsync()
        {
            var user = await _userRepository.AddAsync(_userBuilder.CreateUserWithTasks(2));
            var tasks = user.TaskToDo;
            var result = await _taskToDoRepository.GetByIdUserAsync(tasks.FirstOrDefault().Id);

            Assert.AreEqual(result.UserId, user.Id);
        }
    }
}
