using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var userId = HttpContext.User.Identity.Name;
            var executionTime = DateTime.Now;
            var message = $"The Index action was called by user '{userId}' at '{executionTime}'.";

            _logger.LogInformation(message);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string routeShortName)
        {
            var userId = HttpContext.User.Identity.Name;
            var executionTime = DateTime.Now;
            string message = "";

            Models.Route[]? getRoutes = await _repository.GetRoutesAsync();
            Models.Route? getTheRoute = getRoutes.FirstOrDefault(r => r.RouteShortName == routeShortName);
            Trip[]? getTrips = await _repository.GetTripsAsync();
            Trip? getTheTrip = getTrips.FirstOrDefault(t => t.RouteId == getTheRoute.RouteId);
            message = $"The Index action was called by user '{userId}' at '{executionTime}'. routeShortName = {routeShortName}. Try to get Trip getTheTrip.";
            _logger.LogInformation(message);

            if (getTheTrip != null)
            {
                message = $"The Index action was called by user '{userId}' at '{executionTime}'. routeShortName = {routeShortName}. Trip getTheTrip were retrieved successfully.";
                _logger.LogInformation(message);
                Dictionary<string, string> model = new Dictionary<string, string> {
                    { "routeShortName", routeShortName },
                    { "tripId", getTheTrip.TripId.ToString() },
                    { "shapeId", getTheTrip.ShapeId.ToString() },
                    { "routeId", getTheTrip.RouteId.ToString() }
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
        //[HttpPost]
        //public async Task<IActionResult> IndexAsync(string tripId)
        //{
        //    var userId = HttpContext.User.Identity.Name;
        //    var executionTime = DateTime.Now;
        //    string message = "";

        //    Trip? getTheTrip = await _repository.GetTheTripAsync(tripId);
        //    message = $"The Index action was called by user '{userId}' at '{executionTime}'. tripId = {tripId}. Try to get Trip getTheTrip.";
        //    _logger.LogInformation(message);

        //    if (getTheTrip != null)
        //    {
        //        message = $"The Index action was called by user '{userId}' at '{executionTime}'. tripId = {tripId}. Trip getTheTrip were retrieved successfully.";
        //        _logger.LogInformation(message);
        //        Dictionary<string, string> model = new Dictionary<string, string> {
        //            { "tripId", tripId },
        //            { "shapeId", getTheTrip.ShapeId.ToString() },
        //            { "routeId", getTheTrip.RouteId.ToString() }
        //        };

        //        message = $"The Index action was called by user '{userId}' at '{executionTime}'. The trip details were retrieved successfully.";
        //        _logger.LogInformation(message);

        //        return View(model);
        //    }
        //    else
        //    {
        //        message = $"The Index action was called by user '{userId}' at '{executionTime}'. Failed to retrieve trip details.";
        //        _logger.LogWarning(message);

        //        return RedirectToAction("Error", "Home");
        //    }
        //}

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