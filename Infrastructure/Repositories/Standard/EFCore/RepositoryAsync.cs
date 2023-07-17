using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Interface;
using Infrastructure.Interfaces.Repositories.EFCore;
using Infrastructure.Interfaces.Repositories.Standard;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Standard.EFCore
{
    public class RepositoryAsync<TEntity> : SpecificMethods<TEntity>, IRepositoryAsync<TEntity> where TEntity : class, IIdentityEntity
    {
        /**
         * Recebemos o contexto do banco de dados no construtor da classe, neste caso poderíamos optar por receber ApplicationContext e
         * forçarmos que essa implementação genérica trabalha-se somente com este contexto,
         * entretanto por se tratar de uma implementação genérica optei por receber DbContext
         * possibilitando assim que o contexto de banco de dados seja definido pelas suas classes clientes.
         * Deixaremos todos os métodos que são originados da interface IRepositoryAsync como virtual dessa forma podemos sobrescrevê-los caso seja necessário.
         * O atributo DbSet <TEntity> dbSet é setado para a classe de domínio na qual desejamos realizar as operações de CRUD que neste caso serão User e TaskToDo.
         */

        protected readonly DbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;

        protected RepositoryAsync(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        #region Métodos Private

        private async Task<int> CommitAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        private IQueryable<TEntity> GenerateQueryableWhereExpression(IQueryable<TEntity> query,
            Expression<Func<TEntity, bool>> filter)
        {
            if (filter != null)
                return query.Where(filter);

            return query;
        }

        private IQueryable<TEntity> GenerateIncludeProperties(IQueryable<TEntity> query,
            params string[] includeProperties)
        {
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);
            
            return query;
        }

        #endregion

        #region Métodos Protected

        protected override IQueryable<TEntity> GenerateQuery(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params string[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;
            query = GenerateQueryableWhereExpression(query, filter);
            query = GenerateIncludeProperties(query, includeProperties);

            if(orderBy != null)
                return orderBy(query);

            return query;
        }

        protected override IEnumerable<TEntity> GetYieldManipulated(IEnumerable<TEntity> entities, Func<TEntity, TEntity> doAction)
        {
            foreach (var entity in entities)
                yield return doAction(entity);
        }

        #endregion

        public void Dispose()
        {
            DbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var obj = await DbSet.AddAsync(entity);
            await CommitAsync();
            return obj.Entity;
        }

        public virtual async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
            return await CommitAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(object obj)
        {
            return await DbSet.FindAsync(obj);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(DbSet);
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return await CommitAsync();
        }

        public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
            return await CommitAsync();
        }

        public virtual async Task<bool> RemoveAsync(object obj)
        {
            TEntity entity = await GetByIdAsync(obj);

            if (entity != null) 
                return false;

            return await RemoveAsync(entity) > 0 ? true : false;
        }

        public virtual async Task<int> RemoveAsync(TEntity entity)
        {
            DbSet.Remove(entity);
            return await CommitAsync();
        }

        public virtual async Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            return await CommitAsync();
        }
    }
}
