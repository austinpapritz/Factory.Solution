using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;


namespace Factory.Models;

public class DataInitializer
{
    public static void InitializeData(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<FactoryContext>();
            context.Database.Migrate();

            // If there's already stuff in db, don't run.
            if (context.ExampleModels.Any())
            {
                return;
            }

            var exampleModel = new ExampleModel[]
            {
                new ExampleModel { Name = "Example Name"}
            };

            context.ExampleModels.AddRange(exampleModel);
            context.SaveChanges();
        }
    }
}