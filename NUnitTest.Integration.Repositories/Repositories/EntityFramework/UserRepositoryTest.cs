using NUnit.Framework;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.DBConfiguration.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using NUnitTest.Integration.Repositories.Repositories.DataBuilder;
using NUnitTest.Integration.Repositories.DBConfiguration.EFCore;
using Infrastructure.Repositories.Domain.EFCore;
using Domain.Entities;

namespace NUnitTest.Integration.Repositories.Repositories.EntityFramework
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private ApplicationContext _dbContext;
        private IDbContextTransaction _transaction;

        private IUserRepository _userRepository;
        private UserBuilder _userBuilder;

        [OneTimeSetUp]
        public void GlobalPrepare()
        {
            _dbContext = new EntityFrameworkConnection().DataBaseConfiguration();
        }

        [SetUp]
        public void Inicializar()
        {
            _userRepository = new UserRepository(_dbContext);
            _userBuilder = new();
            _transaction = _dbContext.Database.BeginTransaction();
        }

        [TearDown]
        public void ExecutadoAposExecucaoDeCadaTeste()
        {
            _transaction.Rollback();
        }

        [Test]
        public async Task AddAsync()
        {
            var result = await _userRepository.AddAsync(_userBuilder.CreateUser());
            Assert.Greater(result.Id, 0);
        }

        [Test]
        public async Task AddRangeAsync()
        {
            var result = await _userRepository.AddRangeAsync(_userBuilder.CreateUserList(3));
            Assert.AreEqual(3, result);
        }

        [Test]
        public async Task RemoveAsync()
        {
            var inserted = await _userRepository.AddAsync(_userBuilder.CreateUser());
            var result = await _userRepository.RemoveAsync(inserted.Id);
            Assert.AreEqual(true, result);
        }

        [Test]
        public async Task RemoveRangeAsync()
        {
            var inserted1 = await _userRepository.AddAsync(_userBuilder.CreateUser());
            var inserted2 = await _userRepository.AddAsync(_userBuilder.CreateUser());

            var usersRange = new List<User>()
            {
                inserted1,
                inserted2
            };

            var result = await _userRepository.RemoveRangeAsync(usersRange);
            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task RemoveAsyncObj()
        {
            var inserted = await _userRepository.AddAsync(_userBuilder.CreateUser());
            var result = await _userRepository.RemoveAsync(inserted);
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task UpdateAsync()
        {
            var inserted = await _userRepository.AddAsync(_userBuilder.CreateUser());
            inserted.Name = "Update";
            var result = await _userRepository.UpdateAsync(inserted);
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task UpdateRangeAsync()
        {
            var inserted1 = await _userRepository.AddAsync(_userBuilder.CreateUser());
            var inserted2 = await _userRepository.AddAsync(_userBuilder.CreateUser());
            inserted1.Name = "Update1";
            inserted2.Name = "Update2";

            var userRange = new List<User>()
            {
                inserted1,
                inserted2
            };

            var result = await _userRepository.UpdateRangeAsync(userRange);
            Assert.AreEqual(2, result);
        }

        [Test]
        public async Task GetByIdAsync()
        {
            var user = await _userRepository.AddAsync(_userBuilder.CreateUser());
            var result = await _userRepository.GetByIdAsync(user.Id);
            Assert.AreEqual(result.Id, user.Id);
        }

        [Test]
        public async Task GetAllAsync()
        {
            var user1 = await _userRepository.AddAsync(_userBuilder.CreateUser());
            var user2 = await _userRepository.AddAsync(_userBuilder.CreateUser());
            var result = await _userRepository.GetAllAsync();
            Assert.AreEqual(result.OrderBy(px => px.Id).FirstOrDefault().Id, user1.Id);
            Assert.AreEqual(result.OrderBy(px => px.Id).LastOrDefault().Id, user2.Id);
        }

        [Test]
        public async Task GetAllIncludingTaskAsync()
        {
            var user1 = await _userRepository.AddAsync(_userBuilder.CreateUserWithTasks(1));
            var user2 = await _userRepository.AddAsync(_userBuilder.CreateUserWithTasks(2));
            var result = await _userRepository.GetAllUsersAsync();
            Assert.AreEqual(result.OrderBy(px => px.Id).FirstOrDefault().Id, user1.Id);
            Assert.AreEqual(result.OrderBy(px => px.Id).LastOrDefault().Id, user2.Id);
            Assert.AreEqual(result.OrderBy(px => px.Id).FirstOrDefault().TaskToDo.Count(), 1);
            Assert.AreEqual(result.OrderBy(px => px.Id).LastOrDefault().TaskToDo.Count(), 2);
        }

        [Test]
        public async Task GetByIdIncludingTasksAsync()
        {
            var user = await _userRepository.AddAsync(_userBuilder.CreateUserWithTasks(3));
            var result = await _userRepository.GetByIdAsync(user.Id);
            Assert.AreEqual(result.Id, user.Id);
            Assert.AreEqual(result.TaskToDo.Count(), 3);
        }
    }
}
