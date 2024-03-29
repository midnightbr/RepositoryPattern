﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Entities.Interface;

namespace Domain.Entities
{
    public class User : IIdentityEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private ICollection<TaskToDo> _taskToDo { get; set; }
        public virtual IReadOnlyCollection<TaskToDo> TaskToDo
        {
            get { return _taskToDo as Collection<TaskToDo>; }
        }
        /**
         * observe que TasksToDo é uma propriedade IReadOnlyCollection,
         * desse modo impossibilitamos que classes externas recriem ou
         * alterem a lista de tarefas fora da classe usuário.
         * Este tipo de abordagem é útil quando queremos tratar regras de negócio
         * para que alguma alteração ocorra no objeto de domínio
         */
        public User()
        {
            this._taskToDo = new Collection<TaskToDo>();
        }

        public void AddItemToDo(TaskToDo taskToDo)
        {
            _taskToDo.Add(taskToDo);
        }
    }
}