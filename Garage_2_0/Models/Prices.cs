using System.ComponentModel.DataAnnotations;

namespace Garage_2_0.Models
{
    public class Prices
    {
        public int Id { get; set; }
        public VehicleType Type { get; set; }
        [Range (0, 1000)]
        public int Price { get; set; }
    }
}
