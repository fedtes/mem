using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Mem.Controllers
{

    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            var appRoot = configuration["App:AppRoot"];
            ViewData["AppRoot"] = appRoot;
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}