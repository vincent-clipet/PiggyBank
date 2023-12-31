﻿using Microsoft.AspNetCore.Mvc;
using PiggyBankMVC.Models;
using System.Diagnostics;

namespace PiggyBankMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // return View();
            if (User.IsInRole("Admin") || User.IsInRole("Moderator") || User.IsInRole("Assist"))
                return View();
            else
                return RedirectToAction("Index", "Products");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}