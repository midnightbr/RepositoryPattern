using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Interface;

namespace Infrastructure.Interfaces.Repositories.EFCore
{
    public abstract class SpecificMethods<TEntity> where TEntity : class, IIdentityEntity
    {
        #region MetodoProtected
        /**
        * Primeiro ponto o método GenerateQuery só teria sentido no contexto do Entity Framework, 
          não tendo sentido para quando estivermos implementado repositórios para o ORM Dapper; 
        * Segundo ponto GenerateQuery retorna IQueryable que ao meu ponto de vista não deveria ser exposto a outras camadas 
          que não seja a de repositório, como por exemplo, a camada de serviço que poderia assim 
          definir as queries que seriam executadas no banco de dados, 
          o principal objetivo é tornar nosso código coeso e separar as responsabilidades de cada camada e de cada classe.
         */
        protected abstract IQueryable<TEntity> GenerateQuery(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params string[] includeProperties);

        protected abstract IEnumerable<TEntity> GetYieldManipulated(IEnumerable<TEntity> entities,
            Func<TEntity, TEntity> doAction);

        #endregion
    }
}
