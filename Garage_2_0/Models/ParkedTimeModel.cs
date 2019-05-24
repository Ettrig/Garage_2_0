using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2_0.Models
{
    public enum ParkedType { Bil, Båt, Flygplan, Motorcycel };
    public class ParkedTimeModel
    {
        public int Id { get; set; }
        public ParkedType Type { get; set; }
        public string RegNr { get; set; }
        public string Color { get; set; }
        public DateTime ParkedIn { get; set; }
        public DateTime ParkedOut { get; set; }
        public DateTime ParkedTime { get; set; }
        public TimeSpan TimeSpan { get; set; }
    }
}



