using Domain.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.Standard
{
    public interface IRepositoryAsync<TEntity> : IDisposable where TEntity : class, IIdentityEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetByIdAsync(object obj);
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> RemoveAsync(object obj);
        Task<int> RemoveAsync(TEntity entity);
        Task<int> RemoveRangeAsync(IEnumerable<TEntity> entities);

        /**
         * A interface IRepositoryAsync foi criada como uma interface genérica, a utilização de interfaces,
         * classes e métodos genéricos são uma ótima forma para padronização e reutilização de código
         * para classes que possuem as mesmas características mas diferem em suas instâncias
         */
    }
}
