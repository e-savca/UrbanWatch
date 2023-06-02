﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrbanWatchMVCWebApp.Models;
using UrbanWatchMVCWebApp.Services;
using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITranzyService _tranzyService;
        private readonly TranzyAdapter _tranzyAdapter;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _tranzyService = new TranzyServiceLocal();
            _tranzyAdapter = new TranzyAdapter(_tranzyService);
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string routeId, bool OnWay)
        {
            ViewBag.TypeOfData = 1;
            string param = "0";
            if (OnWay)
            {
                param = "0";
            }
            else
            {
                param = "1";
            }
            return View(_tranzyAdapter.GetDataContext($"{routeId}_{param}"));
        }
        public IActionResult About()
        {
            return View();
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