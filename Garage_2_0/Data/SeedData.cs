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
                //for (int i = 0; i < 100; i++)
                //{
                //    string regNo = "";
                //    for (int i = 0; i < 3; i++)
                //    {
                //        var letter = (char)rnd.New

                //    }
                //    var RegNr = rnd.
                //    var color = colors[rnd.Next(0, colors.Count + 1)];
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
            //}
            //var textInfo = new CultureInfo("en-us", false).TextInfo;
            //var courses = new List<Course>();

            //for (int i = 0; i < 10; i++)
            //{
            //    var course = new Course
            //    {
            //        Title = textInfo.ToTitleCase(Faker.Company.CatchPhrase())
            //    };
            //    courses.Add(course);
            //}
            //context.Course.AddRange(courses);
            //context.SaveChanges();
            //var enrollments = new List<Enrollment>();
            //foreach (var student in students)
            //{
            //    foreach (var course in courses)
            //    {
            //        if (Faker.RandomNumber.Next(5) == 0)
            //        {
            //            var enrollment = new Enrollment
            //            {
            //                Course = course,
            //                Student = student,
            //                Grade = Faker.RandomNumber.Next(1, 6)
            //            };
            //            enrollments.Add(enrollment);
            //        }
            //    }
            //}
            context.SaveChanges();
        }

    }


}
}
