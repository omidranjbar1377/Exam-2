using Exam_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_2.Repositories
{
    public class UserRepo
    {
        private List<User> _users;
        public UserRepo() 
        {
            // InMemoryDataBase.Create(); must move to The Controllers
            _users = InMemoryDataBase.Users;
        }

        public User Get(string username, string password)
        {
            return _users.First(u => u.Username == username && u.Password == password);
        }
    }
}