using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.Models;
using UrbanWatchMVCWebApp.Services;

namespace UrbanWatchMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;

        public HomeController(IRepository repository, ILogger<HomeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            _logger.LogInformation($"The Index action was called at {DateTime.Now}");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string routeName, bool tripType)
        {
            string[] routeNameStrings = routeName.Split(' ');
            string routeShortName = routeNameStrings[2];
            string routeType = (routeNameStrings[0] == "Bus") ? "3" : "11";

            Models.Route[]? getRoutes = await _repository.GetRoutesAsync();
            Models.Route? getTheRoute = getRoutes.FirstOrDefault(r => r.RouteShortName == routeShortName && r.RouteType == routeType);
            if (routeType == "11" && routeShortName == "2")
            {
                tripType = false;
            }
            Trip[]? getTrips = await _repository.GetTripsAsync();
            string tripTypeString = tripType ? "1" : "0";

            Trip? getTheTrip = getTrips.FirstOrDefault(t => t.RouteId == getTheRoute.RouteId && t.DirectionId == tripTypeString);

            _logger.LogInformation($"The Index action was called at '{DateTime.Now}'. routeShortName = {routeShortName}. Try to get Trip getTheTrip.");

            if (getTheTrip != null)
            {
                _logger.LogInformation($"The Index action was called at '{DateTime.Now}'. routeShortName = {routeShortName}. Trip getTheTrip were retrieved successfully.");
                Dictionary<string, string> model = new Dictionary<string, string> {
                    { "routeName", routeName },
                    { "tripId", getTheTrip.TripId.ToString() },
                    { "shapeId", getTheTrip.ShapeId.ToString() },
                    { "routeId", getTheTrip.RouteId.ToString() },
                    { "tripType", tripType.ToString() }
                };

                return View(model);
            }
            else
            {
                _logger.LogWarning($"The Index action was called at '{DateTime.Now}'. Failed to retrieve trip details.");

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