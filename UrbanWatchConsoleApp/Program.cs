using UrbanWatchMVCWebApp;
using UrbanWatchMVCWebApp.Models;
using UrbanWatchMVCWebApp.Models.DataTypes;
using UrbanWatchMVCWebApp.Services;
using UrbanWatchMVCWebApp.Tools;

//double lat1 = 46.98862; // 46.98862, 28.88614
//double lon1 = 28.88614;

//double lat2 = 47.00633; // 47.00633, 28.86642
//double lon2 = 28.86642;

//double distance = HaversineDistanceCalculator.CalculateHaversineDistance(lat1, lon1, lat2, lon2);

TranzyService tranzyService = new TranzyService();
Shape[] shapes = tranzyService.GetShapesData();

Console.WriteLine("The Shape's IDs");
string[] shapeIDs = new string[shapes.Length];
for (int i = 0; i < shapes.Length; i++)
{
    shapeIDs[i] = shapes[i].shapeId;
}
string[] shapeIDsUnique = shapeIDs.Distinct().ToArray();
foreach (var item in shapeIDsUnique)
{
    Console.WriteLine(item);
}
                     
Console.Write("Select the Shape ID: ");
string readString = "6_0";
if (readString != null)
{
    Shape[] newShapes = shapes.Where(item => item.shapeId == readString).ToArray();
    for (int i = 0; i < newShapes.Length; i++)
    {
        if (i != 0)
        {
            double distance = HaversineDistanceCalculator.CalculateHaversineDistance(newShapes[i - 1].shapePointLat, newShapes[i - 1].shapePointLon, newShapes[i].shapePointLat, newShapes[i].shapePointLon);
            Console.WriteLine($"[{i - 1}] [{i}] => {distance} metri");
        }
        else
        {
            Console.WriteLine("Primul punct in sir!");
        }
    }
}
else
{
    Console.WriteLine("readString is null");
}


