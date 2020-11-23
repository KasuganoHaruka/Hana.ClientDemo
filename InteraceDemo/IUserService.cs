using ModelDemo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InteraceDemo
{
    public interface IUserService
    {
        Task<User> FindUser(int id);
        Task<IEnumerable<User>> FindAll();
    }
}
