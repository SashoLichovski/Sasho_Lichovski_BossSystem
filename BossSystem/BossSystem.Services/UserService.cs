using BossSystem.Data;
using BossSystem.Repositories.Interfaces;
using BossSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BossSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User GetCurrentUser(int id)
        {
            return userRepository.GetUserById(id);
        }
        public bool UpdateUsername(User user, string newUsername)
        {
            var iUser = userRepository.GetUserById(user.Id);
            var checkUsername = userRepository.GetUserByUsername(newUsername);
            if (checkUsername == null)
            {
                iUser.Username = newUsername;
                userRepository.UpdateUser(iUser);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void UpdatePassword(User user, string newPassword)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            userRepository.UpdateUser(user);
        }

        public void Deposit(User user, double amount)
        {
            user.Account += amount;
            userRepository.UpdateUser(user);
        }
    }
}
