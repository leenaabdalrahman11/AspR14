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
using AspR14.BLL.Common;
namespace AspR14.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(
                        "DefaultConnection"
                    )
                )
            );
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddScoped<
                ICategoryRepository,
                CategoryRepository
            >();

            builder.Services.AddScoped<
                ICategoryService,
                CategoryService
            >();
            builder.Services.AddScoped<IAuthenticationUserService,AuthenticationUserService>();

            builder.Services.AddLocalization(options =>
                options.ResourcesPath = "");
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<ISeedData,RoleSeedData>();
            builder.Services.AddScoped<IEmailSender,EmailSender>();
            const string defaultCulture = "en";

            var supportedCultures = new[]
            {
                new CultureInfo(defaultCulture),
                new CultureInfo("ar")
            };

            builder.Services.Configure<RequestLocalizationOptions>(options =>
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

            var app = builder.Build();

            app.UseRequestLocalization(
                app.Services
                    .GetRequiredService<
                        IOptions<RequestLocalizationOptions>
                    >()
                    .Value
            );

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            using(var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seeders = services.GetServices<ISeedData>();
                foreach(var seeder in seeders)
                {
                   await seeder.DataSeed();
                }
            }

            app.MapControllers();

            app.Run();
        }
    }
}