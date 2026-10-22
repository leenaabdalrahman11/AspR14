using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;

namespace AspR14.PL.Extensions
{
    public static class LocalizationExtension
    {
        public static IServiceCollection AddLocalizationExtension(this IServiceCollection services)
        {
             services.AddLocalization(options =>
                options.ResourcesPath = "");
           const string defaultCulture = "en";

            var supportedCultures = new[]
            {
                new CultureInfo(defaultCulture),
                new CultureInfo("ar")
            };
            
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture =
                    new RequestCulture(defaultCulture);

                options.SupportedCultures =
                    supportedCultures;

                options.SupportedUICultures =
                    supportedCultures;

                options.RequestCultureProviders.Clear();

                options.RequestCultureProviders.Add(
                    new AcceptLanguageHeaderRequestCultureProvider()
                );
            });

            return services;
        }
    }
}