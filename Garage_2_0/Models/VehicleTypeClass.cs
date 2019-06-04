using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2_0.Models
{
    public class VehicleTypeClass
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

    }
}
