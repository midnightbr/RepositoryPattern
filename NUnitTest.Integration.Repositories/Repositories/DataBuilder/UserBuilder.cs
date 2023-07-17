using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTest.Integration.Repositories.Repositories.DataBuilder
{
    public class UserBuilder
    {
        private User _user;
        private List<User> _users;
        private readonly TaskToDoBuilder _taskToDoBuilder;

        public UserBuilder()
        {
            _taskToDoBuilder = new();
        }

        public User CreateUser()
        {
            _user = new()
            {
                Name = "User from Builder"
            };
            return _user;
        }

        public List<User> CreateUserList(int amount)
        {
            _users = new();
            for (int i = 0; i < amount; i++)
            {
                _users.Add(CreateUser());
            }

            return _users;
        }

        public User CreateUserWithTasks(int amountOfTasks)
        {
            _user = CreateUser();
            foreach(var item in _taskToDoBuilder.CreateTaskToDoList(amountOfTasks))
            {
                _user.AddItemToDo(item);
            }
            return _user;
        }

        public List<User> CreateUserListWithTasks(int amountOfTasks, int amountOfUsers) 
        {
            _users = new();
            for(int i = 0; i < amountOfUsers; i++)
            {
                foreach(var item in _taskToDoBuilder.CreateTaskToDoList(amountOfTasks))
                {
                    _user.AddItemToDo(item);
                }
                _users.Add(_user);
            }
            return _users;
        }
    }
}
