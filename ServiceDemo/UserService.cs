using InteraceDemo;
using ModelDemo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceDemo
{
    public class UserService : IUserService
    {
        public async Task<IEnumerable<User>> FindAll()
        {
            return new List<User>() { 
                new User() { ID = 1, Name = DateTime.Now.ToString() },
                new User() { ID = 2, Name = DateTime.Now.ToString() } 
            };
        }

        public async Task<User> FindUser(int id)
        {
            return new User() { ID = id, Name = DateTime.Now.ToString() };
        }

      
    }
}
