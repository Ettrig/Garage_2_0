﻿using System;
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

        public DbSet<Garage_2_0.Models.ParkedVehicleModel> ParkedVehicleModel { get; set; }

        public DbSet<Garage_2_0.Models.ParkVehicleModel> ParkingVehicleModel { get; set; }
    }
}