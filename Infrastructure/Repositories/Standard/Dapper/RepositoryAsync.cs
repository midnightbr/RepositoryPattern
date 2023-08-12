using Domain.Entities.Interface;
using Infrastructure.Interfaces.Repositories.Standard;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Standard.Dapper
{
    public abstract class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : class, IIdentityEntity
    {
        protected readonly IDbConnection _dbConn;


        public Task<TEntity> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(object obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(object obj)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
