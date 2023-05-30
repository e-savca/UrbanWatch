using UrbanWatchMVCWebApp;
using UrbanWatchMVCWebApp.Models;
using UrbanWatchMVCWebApp.Models.DataTypes;
using UrbanWatchMVCWebApp.Services;
using UrbanWatchMVCWebApp.Tools;


TranzyService tranzyService = new TranzyService();
Shape[] shapes = tranzyService.GetShapesData();

Console.WriteLine("The Shape's IDs");
string[] shapeIDs = new string[shapes.Length];
for (int i = 0; i < shapes.Length; i++)
{
    shapeIDs[i] = shapes[i].Id;
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
    Shape[] newShapes = shapes.Where(item => item.Id == readString).ToArray();
    for (int i = 0; i < newShapes.Length; i++)
    {
        if (i != 0)
        {
            float distance = HaversineDistanceCalculator.CalculateHaversineDistance(newShapes[i - 1].Latitude, newShapes[i - 1].Longitude, newShapes[i].Latitude, newShapes[i].Longitude);
            Console.WriteLine($"[{newShapes[i - 1].Latitude}, {newShapes[i - 1].Longitude}]\t[{newShapes[i].Latitude}, {newShapes[i].Longitude}]\t[{i - 1}] [{i}] => {distance} metri");
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


