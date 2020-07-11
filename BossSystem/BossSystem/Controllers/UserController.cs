using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BossSystem.Helpers;
using BossSystem.Services.Interfaces;
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
    }
}
