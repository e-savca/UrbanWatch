using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanWatchMVCWebApp.Models.DataTypes;

namespace UrbanWatchMVCWebApp.Models
{
    public class DataContext
    {
        public DataTypes.Route[]? routes;
        public DataTypes.Vehicle[]? vehicles;
        public DataTypes.Shape[]? shapes;
        

    }
}