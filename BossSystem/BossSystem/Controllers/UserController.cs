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
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult UserOverview()
        {
            var dbUsers = userService.GetAll();
            var viewModelUsers = dbUsers.Select(x => ConvertTo.UserOverviewModel(x)).ToList();
            return View(viewModelUsers);
        }
        public IActionResult UserDetails()
        {
            var currentUserId = int.Parse(User.FindFirst("Id").Value);
            var user = userService.GetCurrentUser(currentUserId);
            var converted = ConvertTo.UserDetailsModel(user);
            ViewBag.header = $"Hi {converted.Username} !";
            return View(converted);
        }
        public IActionResult ChangePassword(int id)
        {
            ViewBag.header = "Enter your new password";
            var user = userService.GetCurrentUser(id);
            var converted = ConvertTo.ChangePasswordModel(user);
            return View(converted);
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            ViewBag.header = "Enter your new password";
            if (ModelState.IsValid)
            {
                var user = userService.GetCurrentUser(model.Id);
                if (BCrypt.Net.BCrypt.Verify(model.OldPassword, user.Password))
                {
                    userService.UpdatePassword(user, model.Password);
                    return RedirectToAction("UserDetails", "User");
                }
                ModelState.AddModelError(string.Empty, "Old password does not match");
                return View(model);
            }
            return View(model);
        }
        public IActionResult ChangeUsername(int id)
        {
            ViewBag.header = "Enter your new username";
            var user = userService.GetCurrentUser(id);
            var converted = ConvertTo.ChangeUsernameModel(user);
            return View(converted);
        }
        [HttpPost]
        public IActionResult ChangeUsername(ChangeUsernameModel model)
        {
            ViewBag.header = "Enter your new username";
            if (ModelState.IsValid)
            {
                var user = ConvertTo.UserEntity(model);
                var status = userService.UpdateUsername(user, model.Username);
                if (status)
                {
                    return RedirectToAction("UserDetails", "User");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Username '{model.Username}' already exists");
                    return View(model);
                }
            }
            return View(model);
        }

        public IActionResult Deposit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Deposit(double amount, int id)
        {
            var user = userService.GetCurrentUser(id);
            userService.Deposit(user, amount);
            return RedirectToAction("UserDetails");
        }
    }
}
