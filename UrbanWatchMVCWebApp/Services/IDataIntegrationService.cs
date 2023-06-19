using Microsoft.EntityFrameworkCore;
using System.Linq;
using UrbanWatchMVCWebApp.EF;
using UrbanWatchMVCWebApp.Models;

namespace UrbanWatchMVCWebApp.Services
{
    public interface IDataIntegrationService
    {        
        public Task UpdateData();
        public Task InitializeData();
        public bool IsDuplicateVehicle(Vehicle[] oldData, Vehicle[] NewData);
    }
}
