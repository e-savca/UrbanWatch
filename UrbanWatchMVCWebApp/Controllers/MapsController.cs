//using Microsoft.AspNetCore.Mvc;
//using UrbanWatchMVCWebApp.Services;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using UrbanWatchMVCWebApp.Models;

//namespace UrbanWatchMVCWebApp.Controllers
//{
//    public class MapsController : Controller
//    {
//        private readonly TranzyService _tranzyService;
//        private readonly TranzyAdapter _tranzyAdapter;

//        public MapsController()
//        {
//            _tranzyService = new TranzyService();
//            _tranzyAdapter = new TranzyAdapter(_tranzyService);
//        }
//        [HttpGet("maps")] 
//        public IActionResult Index()
//        {
//            ViewBag.JsonModel = JsonConvert.SerializeObject(_tranzyAdapter.GetExtendedVehicles());
//            return View();
//        }
//        [HttpGet("maps/{id?}")]
//        public IActionResult Index(string id)
//        {
//            return View(_tranzyAdapter.GetExtendedVehicles(id));
//        }

//        [HttpGet("maps/routes")]
//        public IActionResult Routes()
//        {
//            return View(_tranzyAdapter.GetRoutes());
//        }
//        [HttpGet("maps/routes/{id?}")]
//        public IActionResult Routes(string id)
//        {
//            return View(_tranzyService.GetShapesData(id));
//        }

//        [HttpGet("maps/stops")]
//        public IActionResult Stops()
//        {
//            return View(_tranzyService.GetStopsData());
//        }
//    }
//}
