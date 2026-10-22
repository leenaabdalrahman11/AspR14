using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspR14.PL.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServicesExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDatabaseExtensions(configuration);  
           services.AddIdentityExtensions();

            services.AddApplicationServicesExtensions();
           services.AddAuthenticationExtensions(configuration);
            services.AddLocalizationExtension();

            return services;
        }
    }
}