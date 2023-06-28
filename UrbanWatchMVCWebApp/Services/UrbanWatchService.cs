using UrbanWatchMVCWebApp.Models.Enums;
using UrbanWatchMVCWebApp.Models.UiModels;
using UrbanWatchMVCWebApp.Services.Interfaces;

namespace UrbanWatchMVCWebApp.Services
{
    public class UrbanWatchService
    {
        private readonly IRepository _repository;
        private readonly ILogger<UrbanWatchService> _logger;

        public UrbanWatchService(IRepository repository, ILogger<UrbanWatchService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public string RouteNameCombine(RouteType? routeType, string routeShortName, string routeLongName)
        {
            return $"{routeType} No {routeShortName} - {routeLongName}";
        }
        public Dictionary<string, string> RouteNameSplit(string combinedRouteName)
        {
            List<string> routeNameStringsList = combinedRouteName.Split(' ').ToList();

            return new Dictionary<string, string>
            {
                { "routeType", routeNameStringsList[0] },
                { "routeShortName", routeNameStringsList[2] }
            };
        }
        public bool SetTripTypeForExceptionVehicles(RouteType routeType, string routeShortName, bool tripType)
        {
            if (routeType == RouteType.Trolleybus && routeShortName == "2")
            {
                return false;
            }
            else
            {
                return tripType;
            }
        }
        public string TripTypeToString(bool tripType)
        {
            return tripType ? "1" : "0";
        }

        public async Task<string?> GetTheRouteIdAsync(string routeShortName, RouteType routeType)
        {
            var getRoutes = await _repository.GetRoutesAsync();
            var theRoute = getRoutes.FirstOrDefault(r => r.RouteShortName == routeShortName && r.RouteType == routeType);
            return theRoute?.RouteId;
        }
        public async Task<Trip?> GetTheTripAsync(string? theRouteId, string tripTypeString)
        {
            var getTrips = await _repository.GetTripsAsync();
            return getTrips.FirstOrDefault(t => t.RouteId == theRouteId && t.DirectionId == tripTypeString);
        }   
    }
}
