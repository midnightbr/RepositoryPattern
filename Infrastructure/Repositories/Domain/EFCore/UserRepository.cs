using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Standard.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Domain.EFCore
{
    public class UserRepository : DomainRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            IQueryable<User> query = await Task.FromResult(GenerateQuery(filter: null, orderBy: null, includeProperties: nameof(User.TaskToDo)));
            return query.ToList();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            IQueryable<User> query = await Task.FromResult(GenerateQuery(filter: (user => user.Id == id), orderBy: null, includeProperties: nameof(User.TaskToDo)));
            return query.SingleOrDefault();
        }
    }
}
