using Microsoft.AspNetCore.Mvc;
using UrbanWatchMVCWebApp.DataTypes;
using UrbanWatchMVCWebApp.Services;

namespace UrbanWatchMVCWebApp.Controllers
{
    public class MapsController : Controller
    {
        private TranzyAdapter _tranzyAdapter = new TranzyAdapter(new TranzyService());

        public IActionResult Index()
        {            
            return View(_tranzyAdapter.GetExtendedVehicles());
        }
        //public IActionResult Index(int id)
        //{

        //    return View(_tranzyService.GetData());
        //}
    }
}
