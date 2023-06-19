﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public class MemoryIntegrationService : IDataIntegrationService
    {
        private DataContext _dataContext;
        private readonly IDataProviderService _dataProviderService;
        private readonly ILogger<MemoryIntegrationService> _logger;
        private int _executionCount;

        public MemoryIntegrationService(DataContext dataContext, IDataProviderService dataProviderService, ILogger<MemoryIntegrationService> logger)
        {
            _dataContext = dataContext;
            _dataProviderService = dataProviderService;
            _logger = logger;
        }
        public async Task UpdateData()
        {
            var count = Interlocked.Increment(ref _executionCount);
            _logger.LogInformation($"MemoryIntegrationService is working. Count: {count}");

            try
            {
                Vehicle[]? vehiclesFromService = await _dataProviderService.GetVehiclesDataAsync();

                _logger.LogInformation($"Call IsDuplicateVehicle. Count: {count} {DateTime.Now}");
                if (!IsDuplicateVehicle(_dataContext.Vehicles, vehiclesFromService))
                {
                    _logger.LogInformation($"Add data from DataProviderService to DataContext. Count: {count} {DateTime.Now}");
                    _dataContext.Vehicles = vehiclesFromService;

                }
            }
            catch (Exception)
            {
                 throw;
            }

        }
        public async Task InitializeData()
        {
            _logger.LogInformation("MemoryIntegrationService is initialized.");

            _dataContext.Vehicles = await _dataProviderService.GetVehiclesDataAsync();
            _dataContext.Routes = await _dataProviderService.GetRoutesDataAsync();
            _dataContext.Trips = await _dataProviderService.GetTripsDataAsync();
            _dataContext.Shapes = await _dataProviderService.GetShapesDataAsync();
            _dataContext.Stops = await _dataProviderService.GetStopsDataAsync();
            _dataContext.StopTimes = await _dataProviderService.GetStopTimesDataAsync();
        }
        public bool IsDuplicateVehicle(Vehicle[] oldData, Vehicle[] NewData)
        {
            List<string?> oldDataStrings = new List<string?>();
            foreach (Vehicle item in oldData)
            {
                oldDataStrings.Add(item.ToString());
            }
            List<string?> newDataStrings = new List<string?>();
            foreach (Vehicle item in NewData)
            {
                newDataStrings.Add(item.ToString());
            }
            return oldDataStrings.SequenceEqual(newDataStrings);
        }
    }
}
