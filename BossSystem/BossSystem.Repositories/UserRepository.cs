using BossSystem.Data;
using BossSystem.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BossSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BossSystemDbContext context;

        public UserRepository(BossSystemDbContext context)
        {
            this.context = context;
        }

        public User GetUserByUsername(string username)
        {
            var user = context.Users.FirstOrDefault(x => x.Username == username);
            return user;
        }

        public void Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public User GetUserById(int id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }

        public void UpdateUser(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }

        public void RemoveUser(User user)
        {
            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
