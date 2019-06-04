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
                    context.Vehicles.RemoveRange(context.Vehicles);
                    context.Members.RemoveRange(context.Members);
                    context.VehicleTypeClass.RemoveRange(context.VehicleTypeClass);
                }

                var colors = new List<string>() { "Röd", "Blå", "Grön", "Blå", "Gul", "Silver", "Svart", "Vit" };
                var rnd = new Random();
                var color = colors[rnd.Next(0, colors.Count - 1)];

                var members = new List<Member>();
                for (int i = 0; i < 100; i++)
                {
                    var member = new Member()
                    {
                        Name = Faker.Name.FullName()
                    };
                    members.Add(member);
                }

                var types = new Dictionary<string, int>() { {"Bil", 50}, { "Motorcykel", 20} , { "Båt", 100}, { "Flygplan", 1000}, { "Buss", 400} };
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
                context.AddRange(members);
                context.AddRange(vehicleTypeClasses);

                context.SaveChanges();
            }
        }
    }
}
