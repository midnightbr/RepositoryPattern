using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain.Standard;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface ITaskToDoRepository : IDomainRepository<TaskToDo>
    {
        Task<IEnumerable<TaskToDo>> GetAllUserAsync();
        Task<TaskToDo> GetByIdUserAsync(int id);

        /**
         *Repare que as interfaces de domínio possuem dois métodos específicos e eles diferem entre si,
         * fizemos isso com o objetivo de possibilitar a extensão da interface genérica de modo
         * que todas as interfaces de domínio possam permanecer coesas, ao fazer isso ficamos aderentes a um dos
         * princípios do SOLID — Interface Segregation que diz que classes não devem depender de métodos que não usam
         */
    }
}
