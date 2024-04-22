using System.ComponentModel.DataAnnotations;

namespace ExerciseGarage2Lexicon.Models.ViewModels
{
    public class OverviewStatictics
    {
        public Dictionary<string, int> VehicleTypeCounts { get; set; }
        public int TotWheels { get; set; }
        public double TotMoneyFromVehicles { get; set; }

        //add more properties if you want more statistics

    }
}
