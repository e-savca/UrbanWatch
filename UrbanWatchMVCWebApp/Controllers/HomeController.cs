using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UrbanWatchMVCWebApp.Models;
using UrbanWatchMVCWebApp.Services;
using Newtonsoft.Json;

namespace UrbanWatchMVCWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITranzyService _tranzyService;
        private readonly TranzyAdapter _tranzyAdapter;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _tranzyService = new TranzyServiceWebAPI();
            _tranzyAdapter = new TranzyAdapter(_tranzyService);
        }
        [HttpGet("/")]
        public IActionResult Index()
        {
            ViewBag.TypeOfData = 0;
            return View(_tranzyAdapter.GetRoutes());
        }
        [HttpGet("/{id}")]
        public IActionResult Index(string id)
        {
            ViewBag.TypeOfData = 1;
            return View(_tranzyAdapter.GetDataContext(id));
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}