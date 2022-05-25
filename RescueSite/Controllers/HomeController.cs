﻿using Microsoft.AspNetCore.Mvc;

namespace RescueSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return Redirect("/User/Login");
        }
    }
}
