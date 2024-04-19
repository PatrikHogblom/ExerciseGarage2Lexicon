using System.ComponentModel.DataAnnotations;

namespace ExerciseGarage2Lexicon.Models.ViewModels
{
    public class SummaryViewModel
    {
        [Display(Name = "Vehicle Type")]
        public string VehicleType { get; set; }

        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        [Display(Name = "Arrival Time")]
        public DateTime ArrivalTime { get; set; }

        [Display(Name = "Minutes Parked")]
        public double TotalParkingTime { get; set; } 
    }
}
