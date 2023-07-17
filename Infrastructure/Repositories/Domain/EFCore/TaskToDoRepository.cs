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
    public class TaskToDoRepository : DomainRepository<TaskToDo>, ITaskToDoRepository
    {
        public TaskToDoRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<TaskToDo>> GetAllUserAsync()
        {
            IQueryable<TaskToDo> query = await Task.FromResult(GenerateQuery(null, null, nameof(TaskToDo.User)));
            return query.ToList();
        }

        public async Task<TaskToDo> GetByIdUserAsync(int id)
        {
            IQueryable<TaskToDo> query = await Task.FromResult(GenerateQuery(taskToDo => taskToDo.Id == id, null, nameof(TaskToDo.User)));
            return query.SingleOrDefault();
        }
    }
}
