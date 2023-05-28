using UrbanWatchMVCWebApp.Models;
using UrbanWatchMVCWebApp.Models.DataTypes;

namespace UrbanWatchMVCWebApp.Services
{
    public class TranzyAdapter
    {
        private TranzyService _tranzyService;
        private Vehicle[] _vehicles;
        private Models.DataTypes.Route[] _routes;
        private ExtendedVehicle[] _extendedVehicles;
        public TranzyAdapter(TranzyService tranzyService)
        {
            _tranzyService = tranzyService;
            _vehicles = _tranzyService.GetVehiclesData();
            _routes = _tranzyService.GetRoutesData();
            _extendedVehicles = new ExtendedVehicle[_vehicles.Length];
            ConvertTypes();
        }
        public ExtendedVehicle[] GetExtendedVehicles()
        {
            return _extendedVehicles;
        }
        public ExtendedVehicle[] GetExtendedVehicles(string id)
        {
            return _extendedVehicles.Where(item => item.routeShortName == id).ToArray();
        }
        private void ConvertTypes()
        {
            for (int i = 0; i < _vehicles.Length; i++)
            {
                _extendedVehicles[i] = new ExtendedVehicle(_vehicles[i]);
                if (_vehicles[i].routeId != null)
                    _extendedVehicles[i].AddRouteData(_routes.Where(item => item.routeId == _vehicles[i].routeId).First());
                           
            }
        }

    }
}
