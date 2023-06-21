﻿using Microsoft.AspNetCore.Routing;
using System.Linq;
using UrbanWatchMVCWebApp.Controllers;
using UrbanWatchMVCWebApp.Models;

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

        public string RouteNameCombine(RouteType? routeType, string RouteShortName, string RouteLongName)
        {
            return $"{routeType} No {RouteShortName} - {RouteLongName}";
        }
        public Dictionary<string, string> RouteNameSplit(string CombinedRouteName)
        {
            List<string> routeNameStringsList = CombinedRouteName.Split(' ').ToList();

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
            Models.Route[]? getRoutes = await _repository.GetRoutesAsync();
            Models.Route? theRoute = getRoutes.FirstOrDefault(r => r.RouteShortName == routeShortName && r.RouteType == routeType);
            return theRoute.RouteId;
        }
        public async Task<Trip?> GetTheTripAsync(string? theRouteId, string tripTypeString)
        {
            Trip[]? getTrips = await _repository.GetTripsAsync();
            return getTrips.FirstOrDefault(t => t.RouteId == theRouteId && t.DirectionId == tripTypeString);
        }


    }
}
