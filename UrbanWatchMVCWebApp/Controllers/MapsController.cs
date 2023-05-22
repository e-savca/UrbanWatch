using Microsoft.AspNetCore.Mvc;
using UrbanWatchMVCWebApp.DataTypes;
using UrbanWatchMVCWebApp.Services;

namespace UrbanWatchMVCWebApp.Controllers
{
    public class MapsController : Controller
    {
        private TranzyService _tranzyService = new TranzyService();
        public IActionResult Index()
        {            
            return View(_tranzyService.GetVehiclesData());
        }
        //public IActionResult Index(int id)
        //{

        //    return View(_tranzyService.GetData());
        //}
    }
}
