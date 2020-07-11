using BossSystem.Data;
using BossSystem.Repositories.Interfaces;
using BossSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BossSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> SignInAsync(User user, HttpContext httpContext)
        {
            var iUser = userRepository.GetUserByUsername(user.Username);
            if (iUser != null && BCrypt.Net.BCrypt.Verify(user.Password, iUser.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, iUser.Username),
                    new Claim(ClaimTypes.Name, iUser.Username),
                    new Claim("Id", iUser.Id.ToString()),
                    new Claim("Role", iUser.Role)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await httpContext.SignInAsync(principal);

                return true;
            }
            return false;
        }

        public async Task SignOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
        }

        public bool SignUp(User user)
        {
            var newUser = userRepository.GetUserByUsername(user.Username);
            if (newUser == null)
            {
                user.Role = "user";
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                userRepository.Add(user);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
