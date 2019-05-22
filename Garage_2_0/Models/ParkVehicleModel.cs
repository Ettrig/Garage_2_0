using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2_0.Models
{
    public class ParkVehicleModel
    {
        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public string RegNr { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int NoWheels { get; set; }
        public string FreeText { get; set; }
    }
}
