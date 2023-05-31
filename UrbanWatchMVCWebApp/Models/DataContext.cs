using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanWatchMVCWebApp.Services;

namespace UrbanWatchMVCWebApp.Models
{
    public class DataContext
    {
        public Route? theRoute { get; set; }
        public Shape[]? Shapes { get; set; }
        public Stop[]? Stops { get; set; }
        public Trip? theTrip { get; set; }
        public Vehicle[]? Vehicles{ get; set; }
    }
}