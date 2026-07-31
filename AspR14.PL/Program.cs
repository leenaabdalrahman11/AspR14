using AspR14.BLL.Services;
using AspR14.DAL.Data;
using AspR14.DAL.Repositories;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;
using AspR14.BLL.Services;
using AspR14.DAL.Repositories;
using AspR14.DAL.Models;
using Microsoft.AspNetCore.Identity;
namespace AspR14.PL
{
    public class Program
    {
        public static void Main(string[] args)
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
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>()
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

            app.MapControllers();

            app.Run();
        }
    }
}