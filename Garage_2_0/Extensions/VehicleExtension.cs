using Garage_2_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2_0.Extensions
{
    public static class VehicleExtension
    {
        public static int ParkingTime(this Vehicle vehicle)
        {
            var totalTime = (DateTime.Now - vehicle.ParkedIn).Minutes;
            return totalTime;
        }
    }
}
