using BossSystem.Data;
using BossSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BossSystem.Helpers
{
    public static class ConvertTo
    {
        public static User UserModel(UserModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Password = model.Password
            };
            return user;
        }

        public static User UserEntity(RegisterUserModel model)
        {
            var user = new User()
            {
                Username = model.Username,
                Password = model.Password
            };
            return user;
        }

        public static UserOverviewModel UserOverviewModel(User user)
        {
            return new UserOverviewModel
            {
                Id = user.Id,
                Username = user.Username
            };
        }
    }
}
