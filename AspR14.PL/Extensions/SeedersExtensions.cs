using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using AspR14.PL.Utils;

namespace AspR14.PL.Extensions
{
    public static class SeedersExtensions
    {
        public static async Task SeedDataAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeders = services.GetServices<ISeedData>();
                foreach (var seeder in seeders)
                {
                    await seeder.DataSeed();
                }
            }
        }
    }
}