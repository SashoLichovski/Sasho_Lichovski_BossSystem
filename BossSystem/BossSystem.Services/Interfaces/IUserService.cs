using BossSystem.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BossSystem.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetAll();
    }
}
