using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace UrbanWatchMVCWebApp.Tools
{
    public class HaversineDistanceCalculator
    {
        private const float EarthRadiusInKm = 6371.0F;

        public static float CalculateHaversineDistance(string LA1, string LO1, string LA2, string LO2)
        {
            float lat1 = float.Parse(LA1, CultureInfo.InvariantCulture);
            float lon1 = float.Parse(LO1, CultureInfo.InvariantCulture);
            float lat2 = float.Parse(LA2, CultureInfo.InvariantCulture);
            float lon2 = float.Parse(LO2, CultureInfo.InvariantCulture);

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            float c = (float)(2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a)));
            float distance = EarthRadiusInKm * c;
            distance *= 1000;
            Math.Round(distance, 1);
            float result = (float)distance;
            return result;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
