using Newtonsoft.Json;
using System.Text;
using UrbanWatchMVCWebApp.Models.UiModels;
using UrbanWatchMVCWebApp.Services.Interfaces;

namespace UrbanWatchMVCWebApp.Services
{
    public class LeafletJSService
    {
        private readonly IRepository repo;
        private StringBuilder script;

        public LeafletJSService(IRepository repository)
        {
            repo = repository;
            script = new StringBuilder();
        }
        public string GenerateMapScript()
        {            
            InitializeMap();
            return script.ToString();
        }

        public async Task<string> GenerateMapScriptAsync(Dictionary<string, string> model)
        {
            InitializeMap();

            await AddStopsAsync(model["shapeId"]);
            await AddVehiclesAsync(model["tripId"]);
            await AddPolylineAsync(model["shapeId"], model["routeId"]);            

            FinalizeMap();

            return script.ToString();
        }
        private async Task AddStopsAsync(string shapeId)
        {
            var getStops = await repo.GetStopsAsync(shapeId);
            script.AppendLine($"var busStopIcon = L.divIcon({{ className: 'bus-stop-div-icon', iconSize: new L.Point(10, 10) }});");
            foreach (Stop stop in getStops)
            {
                script.AppendLine($"var marker_{stop.StopId} = L.marker([{stop.Latitude}, {stop.Longitude}], {{ icon: busStopIcon }}).addTo(map);");
                script.AppendLine($"marker_{stop.StopId}.bindPopup('Statia: {stop.Name}');");
                script.AppendLine();
            }
        }
        private async Task AddVehiclesAsync(string tripId)
        {
            var getVehicles = await repo.GetVehiclesAsync(tripId);
            script.AppendLine($"var busIcon = L.divIcon({{ className: 'bus-div-icon', iconSize: new L.Point(20, 20) }});");

            foreach (Vehicle vehicle in getVehicles)
            {
                int diffSeconds = (int)DateTime.Now.AddHours(-3).Subtract(vehicle.Timestamp).TotalSeconds;                
                script.AppendLine($"var marker_{vehicle.VehicleId} = L.marker([{vehicle.Latitude}, {vehicle.Longitude}], {{ icon: busIcon }}).addTo(map);");
                script.AppendLine($"marker_{vehicle.VehicleId}.bindPopup('Vehicul: {vehicle.Label}<br />Speed: {vehicle.Speed}<br />De acum: {diffSeconds} secunde');");
                script.AppendLine();
            }
        }
        private async Task AddPolylineAsync(string shapeId, string routeId)
        {
            var getShapes = await repo.GetShapesAsync(shapeId);
            var routeObject = await repo.GetTheRouteAsync(routeId);
            string? routeColor = routeObject.RouteColor;

            List<string[]> shapesArray = new List<string[]>();

            foreach (Shape shape in getShapes)
            {
                string[] point = new string[2];
                point[0] = shape.Latitude;
                point[1] = shape.Longitude;
                shapesArray.Add(point);
            }

            script.AppendLine($"var shapes = {JsonConvert.SerializeObject(shapesArray)};");
            script.AppendLine($"var polyline = L.polyline(shapes, {{ color: '{routeColor}' }}).addTo(map);");
            script.AppendLine();
        }
        private void InitializeMap()
        {
            script.Clear();

            script.AppendLine("var map = L.map('map').setView([47.02543731466161, 28.830271935332686], 12);");
            script.AppendLine("L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {");
            script.AppendLine("    maxZoom: 19,");
            script.AppendLine("    attribution: '&copy; <a href=\"http://www.openstreetmap.org/copyright\">OpenStreetMap</a>'");
            script.AppendLine("}).addTo(map);");
            script.AppendLine();
        }
        private void FinalizeMap()
        {
            script.AppendLine("map.fitBounds(polyline.getBounds());");
        }
    }

}
