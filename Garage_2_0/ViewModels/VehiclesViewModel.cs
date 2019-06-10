using Garage_2_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2_0.ViewModels
{
    public class VehiclesViewModel
    {
        public int Id { get; set; }
        public string MemberName { get; set; }
        public VehicleTypeClass VehicleTypeClass { get; set; }
        public string RegNr { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int NoWheels { get; set; }
        public int ParkingTime { get; set; }
        public string SearchTerm { get; set; }

    }
}
