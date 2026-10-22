using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace AspR14.PL.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString(
                        "DefaultConnection"
                    )
                )
            );

            return services;
        }
        
    }
}