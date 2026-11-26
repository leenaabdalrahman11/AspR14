using AspR14.BLL.Services;
using AspR14.DAL.Data;
using AspR14.DAL.Repositories;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using AspR14.DAL.Models;
using Microsoft.AspNetCore.Identity;
using AspR14.PL.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AspR14.BLL.Common;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AspR14.PL.Extensions;
using AspR14.BLL.Mapping;

namespace AspR14.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServicesExtensions(builder.Configuration);
            var app = builder.Build();
            app.UseExceptionHandler();
            app.UseRequestLocalization(
                app.Services
                    .GetRequiredService<
                        IOptions<RequestLocalizationOptions>
                    >()
                    .Value
            );
            app.UseRequestLocalization(
            app.Services
               .GetRequiredService<IOptions<RequestLocalizationOptions>>()
               .Value
            );

            app.Use(async (context, next) =>
            {
                CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

                await next();
            });
            MapesterConfig.MapsterConfigRegister();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            //app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            await app.SeedDataAsync();
            app.MapControllers();

            app.Run();
        }
    }
}