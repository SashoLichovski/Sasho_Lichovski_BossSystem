using BossSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BossSystem.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetCurrentUser(int id);
        bool UpdateUsername(User user, string newUsername);
        void UpdatePassword(User user, string newPassword);
        void Deposit(User user, double amount);
    }
}
