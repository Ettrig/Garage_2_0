using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Garage_2_0.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Registreringsnummer krävs, max 6 tecken")]
        [StringLength(6)]
        public string RegNr { get; set; }
        [Required(ErrorMessage = "Skriv in bilens färg")]
        [StringLength(20)]
        public string Color { get; set; }
        [Required(ErrorMessage = "Skriv in bilens märke")]
        [StringLength(40)]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Skriv in bilens modell")]
        [StringLength(20)]
        public string Model { get; set; }
        [Range(0, 40)]
        public int NoWheels { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Parkering påbörjad")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}")]
        public DateTime ParkedIn { get; set; }
        
        public int MemberId { get; set; }
        public Member Member { get; set; }
        public int VehicleTypeClassId { get; set; }
        public VehicleTypeClass VehicleTypeClass { get; set; }
       
    }
}
