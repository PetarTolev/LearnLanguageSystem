﻿namespace LearnLanguageSystem.Web.Controllers
{
    using System.Diagnostics;

    using LearnLanguageSystem.Web.ViewModels;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode == 404)
            {
                return this.RedirectToAction("NotFound", "Errors");
            }

            if (statusCode == 400)
            {
                return this.RedirectToAction("BadRequest", "Errors");
            }

            if (statusCode == 403)
            {
                return this.RedirectToAction("Forbid", "Errors");
            }

            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
