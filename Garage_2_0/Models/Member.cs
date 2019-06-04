using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2_0.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
