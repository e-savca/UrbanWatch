using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrbanWatchMVCWebApp.IServices;
using UrbanWatchMVCWebApp.Models;
using UrbanWatchMVCWebApp.Models.Enums;
using UrbanWatchMVCWebApp.Models.UiModels;
using UrbanWatchMVCWebApp.Services;

namespace UrbanWatchMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UrbanWatchService _urbanWatchService;
        private readonly IDataIntegrationService _dataIntegrationService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(UrbanWatchService urbanWatchService, IDataIntegrationService dataIntegrationService, ILogger<HomeController> logger)
        {
            _urbanWatchService = urbanWatchService;
            _dataIntegrationService = dataIntegrationService;
            _logger = logger;            
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            await _dataIntegrationService.InitializeDataAsync();
            _logger.LogInformation($"The Index action was called at {DateTime.Now}");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(string routeName, bool tripType)
        {
            _logger.LogInformation($"The Index action was called at '{DateTime.Now}'. routeName = {routeName}. Try to get Trip getTheTrip.");

            var routeNameStrings = _urbanWatchService.RouteNameSplit(routeName);

            var routeShortName = routeNameStrings["routeShortName"];
            var routeType = (RouteType)Enum.Parse(typeof(RouteType), routeNameStrings["routeType"]);

            _urbanWatchService.SetTripTypeForExceptionVehicles(routeType, routeShortName, tripType);
            var tripTypeString = _urbanWatchService.TripTypeToString(tripType);

            var theRouteId = await _urbanWatchService.GetTheRouteIdAsync(routeShortName, routeType);
            var getTheTrip = await _urbanWatchService.GetTheTripAsync(theRouteId, tripTypeString);

            if (getTheTrip != null)
            {
                _logger.LogInformation($"The Index action was called at '{DateTime.Now}'. routeShortName = {routeShortName}. Trip getTheTrip were retrieved successfully.");
                var model = new Dictionary<string, string> {
                    { "routeName", routeName },
                    { "tripId", getTheTrip.TripId },
                    { "shapeId", getTheTrip.ShapeId },
                    { "routeId", getTheTrip.RouteId },
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