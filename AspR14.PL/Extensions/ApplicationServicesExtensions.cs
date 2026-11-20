using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspR14.BLL.Common;
using AspR14.BLL.Services;
using AspR14.DAL.Repositories;
using AspR14.PL.Utils;
using KASHOP.PL;

namespace AspR14.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServicesExtensions(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddScoped<IAuthenticationUserService, AuthenticationUserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISeedData, RoleSeedData>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<
                ICategoryService,
                CategoryService
            >();
            services.AddScoped<
                IProductService,
                ProductService>();

            services.AddScoped<IFileService, FileService>();
            services.AddProblemDetails();
            return services;
        }
    }
}