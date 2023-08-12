using Domain.Entities.Interface;
using Infrastructure.Interfaces.Repositories.Domain.Standard;
using Infrastructure.Repositories.Standard.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Standard.Dapper
{
    public abstract class DomainRepository<TEntity> : RepositoryAsync<TEntity>, IDomainRepository<TEntity> where TEntity : class, IIdentityEntity
    {
        protected DomainRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected DomainRepository(IDbConnection databaseConnection, IDbTransaction transaction = null) : base(databaseConnection, transaction) { }
    }
}
