using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2_0.Models
{
    public enum VehicleType { Bil, Båt, Flygplan, Motorcycel};  

    public class ParkedVehicleModel
    {
        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public string RegNr { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int NoWheels { get; set; }
        public string FreeText { get; set; }
        public DateTime ParkedIn { get; set; }
        public DateTime ParkedOut { get; set; }
    }
}
