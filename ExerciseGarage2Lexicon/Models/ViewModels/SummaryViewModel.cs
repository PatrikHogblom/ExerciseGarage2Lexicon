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

        [Display(Name = "Time Parked")]
        public TimeSpan TotalParkingTime { get; set; }

        public string DisplayTime => $"{TotalParkingTime.Days} days, {TotalParkingTime.Hours} h {TotalParkingTime.Minutes} min";
    }
}
