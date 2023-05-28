namespace UrbanWatchMVCWebApp.Tools
{
    public class HaversineDistanceCalculator
    {
        private const double EarthRadiusInKm = 6371.0;

        public static double CalculateHaversineDistance(string LA1, string LO1, string LA2, string LO2)
        {
            double lat1 = double.Parse(LA1);
            double lon1 = double.Parse(LO1);
            double lat2 = double.Parse(LA2);
            double lon2 = double.Parse(LO2);

            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = EarthRadiusInKm * c;

            return distance;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}
