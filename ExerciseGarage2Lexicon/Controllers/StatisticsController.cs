using ExerciseGarage2Lexicon.Data;
using ExerciseGarage2Lexicon.Models.ViewModels;
using ExerciseGarage2Lexicon.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace ExerciseGarage2Lexicon.Controllers
{
    public class StatisticsController : Controller
    {

        private readonly ExerciseGarage2LexiconContext _context;//get the database
        private readonly IPriceService _priceService;// get the price

        public StatisticsController(ExerciseGarage2LexiconContext context, IPriceService priceService)
        {
            _context = context;
            _priceService = priceService;
        }


        public IActionResult Index()
        {
            //first count the total times a type of vehicle is occuring
            var vehicleTypeCounts = _context.ParkedVehicle
                 .GroupBy(v => v.VehicleType)
                 .Select(g => new { vType = g.Key, vCount = g.Count() })
                 .ToList();

            //calculate the total amount of wheels that are parked in the garage
            var vehicleTotWheels = _context.ParkedVehicle.Sum(w => w.NumberOfWheels);

            //Calculate the expected income from all parked Vehicles
            var currentTime = DateTime.Now;
            double totalPriceAllVehicles = _context.ParkedVehicle.ToList()
                .Sum(vehicle => (currentTime - vehicle.ArrivalTime).TotalMinutes * _priceService.GetPrice());


            OverviewStatictics statictics = new OverviewStatictics
            {
                VehicleTypeCounts = vehicleTypeCounts.ToDictionary(x => x.vType, x => x.vCount),
                TotWheels = vehicleTotWheels,
                TotMoneyFromVehicles = Math.Round(totalPriceAllVehicles, 2)
            };

            return View(statictics);
        }
        
    }
}


