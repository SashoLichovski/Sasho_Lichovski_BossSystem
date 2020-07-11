using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BossSystem.Helpers;
using BossSystem.Services.Interfaces;
using BossSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BossSystem.Controllers
{
    public class AuthController : Controller
    {
        public IAuthService AuthService { get; }
        public object ConverTo { get; private set; }

        public AuthController(IAuthService userService)
        {
            AuthService = userService;
        }

        public IActionResult SignIn()
        {
            ViewBag.header = "Sign in";
            var user = new UserModel();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserModel user)
        {
            ViewBag.header = "Sign in";
            if (ModelState.IsValid)
            {
                var converted = ConvertTo.UserModel(user);
                var isValid = await AuthService.SignInAsync(converted, HttpContext);
                if (isValid)
                {
                    return RedirectToAction("HomePage", "Home");
                }
                ModelState.AddModelError("Username", "Inccorect Username or Password");
                return View();
            }
            return View(user);
        }
        public async Task<IActionResult> SignOut()
        {
            await AuthService.SignOutAsync(HttpContext);
            return RedirectToAction("HomePage", "Home");
        }
        public IActionResult Register()
        {
            ViewBag.header = "Register";
            var newUser = new RegisterUserModel();
            return View(newUser);
        }
        [HttpPost]
        public IActionResult Register(RegisterUserModel registerUser)
        {
            if (ModelState.IsValid)
            {
                var user = ConvertTo.UserEntity(registerUser);
                var state = AuthService.SignUp(user);
                if (state)
                {
                    return RedirectToAction("SuccessfullRegister", "Auth");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Username '{registerUser.Username}' already exists");
                    return View(registerUser);
                }

            }
            return View(registerUser);
        }

        public IActionResult SuccessfulRegister()
        {
            ViewBag.header = "Congratulations";
            return View();
        }
    }
}
