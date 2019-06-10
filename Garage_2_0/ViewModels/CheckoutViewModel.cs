using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2_0.ViewModels
{
    public class CheckoutViewModel
    {
        public int Id { get; set; }
        public string Member { get; set; }
        public string VehicleType { get; set; }
        public string RegNr { get; set; }
        public DateTime ParkedIn { get; set; }
        public DateTime ParkedOut { get; set; }
        public int TotalMinutes { get; set; }
        public int Price { get; set; }
        public int Cost { get; set; }
    }
}
