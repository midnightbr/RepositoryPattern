using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Integration.Repositories.Repositories.DataBuilder
{
    public class TaskToDoBuilder
    {
        private TaskToDo _taskToDo;
        private List<TaskToDo> _taskToDoList;

        public TaskToDoBuilder() {}

        public TaskToDo CreateTaskToDo()
        {
            _taskToDo = new() { Title = "Task from Builder", Start = DateTime.Now, DeadLine = DateTime.Now };
            return _taskToDo;
        }

        public TaskToDo CreateTaskToDoWithUser(int id)
        {
            _taskToDo = new() { Title = "Task from Builder", Start = DateTime.Now, DeadLine = DateTime.Now, UserId = id };
            return _taskToDo;
        }

        public List<TaskToDo> CreateTaskToDoList(int amount) 
        {
            _taskToDoList = new();
            for (int i = 0; i < amount; i++)
            {
                _taskToDoList.Add(CreateTaskToDo());
            }
            return _taskToDoList;
        }

        public List<TaskToDo> CreateTaskToDoListWithUser(int amount, int id)
        {
            _taskToDoList = new();
            for(int i = 0;i < amount;i++)
            {
                _taskToDoList.Add(CreateTaskToDoWithUser(id));
            }
            return _taskToDoList;
        }
    }
}
