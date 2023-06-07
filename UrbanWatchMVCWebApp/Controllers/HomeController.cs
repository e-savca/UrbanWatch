using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrbanWatchMVCWebApp.Models;
using UrbanWatchMVCWebApp.Services;
using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly TranzyAdapter _tranzyAdapter;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _tranzyAdapter = new TranzyAdapter();
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string tripId)
        {
            Trip? getTheTrip = await _tranzyAdapter.GetTheTripAsync(tripId);
            Dictionary<string, string> model = new Dictionary<string, string>();
            model["tripId"] = tripId;
            model["shapeId"] = $"{getTheTrip.shapeId}";
            model["routeId"] = $"{getTheTrip.routeId}";

            return View(model);
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