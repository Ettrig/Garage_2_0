using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Garage_2_0.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Garage_2_0.Data
{
    public class SeedData
    {
        internal static void Initialize(IServiceProvider services)
        {
            var options = services.GetRequiredService<DbContextOptions<Garage_2_0Context>>();

            using (var context = new Garage_2_0Context(options))
            {
                if (context.Members.Any())
                {
                    return;
                }
                
                var rnd = new Random();


                // Populate Member
                var members = new List<Member>();
                for (int i = 0; i < 100; i++)
                {
                    var member = new Member()
                    {
                        Name = Faker.Name.FullName()
                    };
                    members.Add(member);
                }
                context.AddRange(members);


                // Populate VehicleTypeClass
                var types = new Dictionary<string, int>() { { "Bil", 50 }, { "Motorcykel", 20 }, { "Båt", 100 }, { "Flygplan", 1000 }, { "Buss", 400 } };
                var vehicleTypeClasses = new List<VehicleTypeClass>();

                foreach (var type in types)
                {
                    var vehicleTypeClass = new VehicleTypeClass()
                    {
                        Type = type.Key,
                        Price = type.Value
                    };
                    vehicleTypeClasses.Add(vehicleTypeClass);
                }
                context.AddRange(vehicleTypeClasses);


                var aToZ = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                var brands = new List<string>() { "Volvo", "Ferrari", "BMW", "Mercedes", "Audi", "Ford", "Mini", "Boeing", "Nimbus" };
                var models = new List<string>() { "XC90", "Testarossa", "M3", "Sport", "A5", "Mustang", "Clubman", "747", "Flybridge" };
                var colors = new List<string>() { "Röd", "Blå", "Grön", "Blå", "Gul", "Silver", "Svart", "Vit" };


                // Populate Vehicle
                var vehicles = new List<Vehicle>();
                foreach (var member in members)
                {
                    if (Faker.RandomNumber.Next(2) == 0)
                    {
                        foreach (var vehicleTypeClass in vehicleTypeClasses)
                        {
                            var randomCarBrandsAndModel = rnd.Next(0, brands.Count);
                            
                            var vehicle = new Vehicle()
                            {
                                RegNr = "" + aToZ[rnd.Next(26)] + aToZ[rnd.Next(26)] + aToZ[rnd.Next(26)] + rnd.Next(0, 9) + rnd.Next(0, 9) + rnd.Next(0, 9),
                                Color = colors[rnd.Next(0, colors.Count)],
                                Brand = brands[randomCarBrandsAndModel],
                                Model = models[randomCarBrandsAndModel],
                                Member = member,
                                VehicleTypeClass = vehicleTypeClass,
                                NoWheels = vehicleTypeClass.Type == "Bil" ? 4 :
                                            vehicleTypeClass.Type == "Motorcykel" ? 2 :
                                            vehicleTypeClass.Type == "Buss" ? 18 :
                                            vehicleTypeClass.Type == "Flygplan" ? 10 : 0,
                                ParkedIn = DateTime.Now
                        };
                            vehicles.Add(vehicle);
                        }
                    }
                }
                context.AddRange(vehicles);
                context.SaveChanges();
            }
        }
    }
}
