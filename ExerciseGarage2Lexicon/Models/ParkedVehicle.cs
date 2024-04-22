using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace ExerciseGarage2Lexicon.Models
{
    [Index(nameof(RegistrationNumber), IsUnique = true)]
    public class ParkedVehicle
    {
        public int Id { get; set; }

        //[Display(Name = "Vehicle Type")]
        public string VehicleType { get; set; }

        //[Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; } // måste vara olika "ingen får vara samma som den andra"
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int NumberOfWheels { get; set; } // får inte vara negativt nummer

        [Display(Name = "Arrival Time")]
        public DateTime ArrivalTime { get; set; } // ska inte gå att editera eller manipulera
        public DateTime CheckOutTime { get; set; }
        public double TotalParkingTimeInMinutes { get; set; }
        public double Price { get; set; } //  1 minute = 1kr
    }
}
