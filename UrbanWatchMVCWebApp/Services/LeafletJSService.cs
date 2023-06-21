using Newtonsoft.Json;
using System.Text;
using UrbanWatchMVCWebApp.Models;

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
            Stop[] getStops = await repo.GetStopsAsync(shapeId);
            foreach (Stop stop in getStops)
            {
                script.AppendLine($"var busStopIcon = L.divIcon({{ className: 'bus-stop-div-icon', iconSize: new L.Point(10, 10) }});");
                script.AppendLine($"var marker = L.marker([{stop.Latitude}, {stop.Longitude}], {{ icon: busStopIcon }}).addTo(map);");
                script.AppendLine($"marker.bindPopup('Statia: {stop.Name}');");
            }
        }
        private async Task AddVehiclesAsync(string tripId)
        {
            Vehicle[] getVehicles = await repo.GetVehiclesAsync(tripId);
            foreach (Vehicle vehicle in getVehicles)
            {
                int diffSeconds = (int)DateTime.Now.AddHours(-3).Subtract(vehicle.Timestamp).TotalSeconds;
                script.AppendLine($"var busIcon = L.divIcon({{ className: 'bus-div-icon', iconSize: new L.Point(20, 20) }});");
                script.AppendLine($"var marker = L.marker([{vehicle.Latitude}, {vehicle.Longitude}], {{ icon: busIcon }}).addTo(map);");
                script.AppendLine($"marker.bindPopup('Vehicul: {vehicle.Label}<br />Speed: {vehicle.Speed}<br />De acum: {diffSeconds} secunde');");
            }
        }
        private async Task AddPolylineAsync(string shapeId, string routeId)
        {
            Shape[] getShapes = await repo.GetShapesAsync(shapeId);
            Models.Route? routeObject = await repo.GetTheRouteAsync(routeId);
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
        }
        private void InitializeMap()
        {
            script.Clear();

            script.AppendLine("var map = L.map('map').setView([47.02543731466161, 28.830271935332686], 12);");
            script.AppendLine("L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {");
            script.AppendLine("    maxZoom: 19,");
            script.AppendLine("    attribution: '&copy; <a href=\"http://www.openstreetmap.org/copyright\">OpenStreetMap</a>'");
            script.AppendLine("}).addTo(map);");
        }
        private void FinalizeMap()
        {
            script.AppendLine("map.fitBounds(polyline.getBounds());");
        }
    }

}
