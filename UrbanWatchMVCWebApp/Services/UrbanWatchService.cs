using Microsoft.AspNetCore.Routing;
using System.Linq;
using UrbanWatchMVCWebApp.Controllers;

namespace UrbanWatchMVCWebApp.Services
{
    public class UrbanWatchService
    {
        private readonly IRepository _repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UrbanWatchService> _logger;

        public UrbanWatchService(IRepository repository, IHttpContextAccessor httpContextAccessor, ILogger<UrbanWatchService> logger)
        {
            _repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public string RouteNameCombine(string routeType, string RouteShortName, string RouteLongName)
        {
            return $"{routeType} No {RouteShortName} - {RouteLongName}";
        }
        public Dictionary<string, string> RouteNameSplit(string CombinedRouteName)
        {
            List<string> routeNameStringsList = CombinedRouteName.Split(' ').ToList();

            return new Dictionary<string, string>
            {
                { "routeType", routeNameStringsList[0] },
                { "RouteShortName", routeNameStringsList[2] },
                { "RouteLongName", routeNameStringsList[3] }
            };
        }



    }
}
