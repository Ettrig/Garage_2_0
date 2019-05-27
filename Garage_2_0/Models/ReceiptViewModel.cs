using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2_0.Models
{
    public class ReceiptViewModel
    {
        public string RegNr { get; set; }
        public DateTime ParkedIn { get; set; }
        public DateTime ParkedOut { get; set; }
        public int Price { get; set; }
        public int Cost { get; set; }
    }
}
