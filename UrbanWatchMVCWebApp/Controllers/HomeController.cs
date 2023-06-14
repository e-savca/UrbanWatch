using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrbanWatchMVCWebApp.Models;
using UrbanWatchMVCWebApp.Services;

namespace UrbanWatchMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITranzyAdapter _tranzyAdapter;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ITranzyAdapter tranzyAdapter, ILogger<HomeController> logger)
        {
            _tranzyAdapter = tranzyAdapter;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var userId = HttpContext.User.Identity.Name;
            var executionTime = DateTime.Now;
            var message = $"The Index action was called by user '{userId}' at '{executionTime}'.";

            _logger.LogInformation(message);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string tripId)
        {
            var userId = HttpContext.User.Identity.Name;
            var executionTime = DateTime.Now;
            string message = "";

            Trip? getTheTrip = await _tranzyAdapter.GetTheTripAsync(tripId);

            if (getTheTrip != null)
            {
                Dictionary<string, string> model = new Dictionary<string, string> {
                    { "tripId", tripId },
                    { "shapeId", getTheTrip.shapeId.ToString() },
                    { "routeId", getTheTrip.routeId.ToString() }
                };

                message = $"The Index action was called by user '{userId}' at '{executionTime}'. The trip details were retrieved successfully.";
                _logger.LogInformation(message);

                return View(model);
            }
            else
            {
                message = $"The Index action was called by user '{userId}' at '{executionTime}'. Failed to retrieve trip details.";
                _logger.LogWarning(message);

                return RedirectToAction("Error", "Home");
            }
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