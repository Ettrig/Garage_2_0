using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Garage_2_0.Models;

namespace Garage_2_0.Models
{
    public class Garage_2_0Context : DbContext
    {
        public Garage_2_0Context (DbContextOptions<Garage_2_0Context> options)
            : base(options)
        {
        }

        protected override void  OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasData(
                 new Vehicle { Id = 1, Brand = "Volvo", Color = "Grön", FreeText = "Hejsan", Model = "Amazon", NoWheels = 4, ParkedIn = DateTime.Today, ParkedOut = DateTime.MaxValue, RegNr = "ABC123", Type = VehicleType.Bil }
                );
        }

        public DbSet<Vehicle> Vehicles{ get; set; }

        public DbSet<Prices> Prices { get; set; }
    }
}
