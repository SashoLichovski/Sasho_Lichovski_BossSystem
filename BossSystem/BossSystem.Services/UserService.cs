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
    }
}
