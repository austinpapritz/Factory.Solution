using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Factory.Models
{
    public class DataInitializer
    {
        public static void InitializeData(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<FactoryContext>();
                context.Database.Migrate();

                // Don't exe if db is already seeded.
                if (!context.Engineers.Any())
                {
                    // List of Licenses.
                    var licenses = new List<License>
                    {
                        new License { Name = "Heavy" },
                        new License { Name = "HighTemp" },
                        new License { Name = "Hydraulics" },
                        new License { Name = "Emissions" }
                    };
                    context.Licenses.AddRange(licenses);
                    context.SaveChanges();

                    // Seeding 5 Engineers with random licenses.
                    var rand = new Random();
                    for (int i = 1; i <= 5; i++)
                    {
                        var engineer = new Engineer
                        {
                            Name = $"Engineer{i}",
                            EngineerLicenses = licenses
                                .Where(l => rand.Next(2) == 0) // For every license, pick num less than 2 (0 or 1), if the random number is 0 include that license.
                                .Select(l => new EngineerLicense { License = l }) // Create an `EngineerLicense` for every License selected above.
                                .ToList()
                        };
                        context.Engineers.Add(engineer);
                    }

                    // Seeding 15 Machines with random licenses.
                    for (int i = 1; i <= 15; i++)
                    {
                        var machine = new Machine
                        {
                            Country = $"Country{i}",
                            Make = $"Make{i}",
                            Model = $"Model{i}",
                            MachineLicenses = licenses
                                .Where(l => rand.Next(2) == 0) // For every license, pick num less than 2 (0 or 1), if the random number is 0 include that license.
                                .Select(l => new MachineLicense { License = l }) // Create a `MachineLicense` for every License selected above.
                                .ToList()
                        };
                        context.Machines.Add(machine);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
