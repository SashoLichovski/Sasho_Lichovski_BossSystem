using BossSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BossSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
        void Add(User user);
        User GetUserById(int id);
        void UpdateUser(User user);
        List<User> GetAll();
        void RemoveUser(User user);
    }
}
