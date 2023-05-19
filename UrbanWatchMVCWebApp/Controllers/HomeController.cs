using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrbanWatchMVCWebApp.Models;
using UrbanWatchMVCWebApp.Services;
using UrbanWatchMVCWebApp.DataTypes;
using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TranzyService _tranzyService = new TranzyService();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Utilizați metoda GetData pentru a obține datele de tipul Vehicles[]
            Vehicle[] vehicles = _tranzyService.GetData();
            //var json = JsonConvert.SerializeObject(vehicles);
            //// Pasați datele în modelul view-ului
            //ViewData["VehiclesText"] = json;

            return View(vehicles);
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