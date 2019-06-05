﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2_0.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string RegNr { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int NoWheels { get; set; }
        public DateTime ParkedIn { get; set; }
        
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int VehicleTypeClassId { get; set; }
        public VehicleTypeClass VehicleType { get; set; }
    }
}
