using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ExerciseGarage2Lexicon.Data;
using ExerciseGarage2Lexicon.Models;
using System.Configuration;
using ExerciseGarage2Lexicon.Services;
namespace ExerciseGarage2Lexicon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ExerciseGarage2LexiconContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ExerciseGarage2LexiconContext") ?? throw new InvalidOperationException("Connection string 'ExerciseGarage2LexiconContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<IPriceService, PriceService>();

            var app = builder.Build();


            //set the inital price of parkinglot
            var initalPrice = 1.0;
            var priceService = app.Services.GetRequiredService<IPriceService>();
            priceService.SetPrice(initalPrice);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=ParkedVehicles}/{action=Index}/{id?}");

           DbInitalizer.Seed(app);

            app.Run();


        }
    }
}
