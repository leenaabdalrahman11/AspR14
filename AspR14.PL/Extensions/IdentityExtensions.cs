using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.DAL.Data;
using AspR14.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace AspR14.PL.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityExtensions(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}