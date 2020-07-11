using BossSystem.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BossSystem.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SignInAsync(User user, HttpContext httpContext);
        Task SignOutAsync(HttpContext httpContext);
        bool SignUp(User user);
    }
}
