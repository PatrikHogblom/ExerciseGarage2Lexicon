using ExerciseGarage2Lexicon.Data;
using System.IO.Pipelines;

namespace ExerciseGarage2Lexicon.Models
{
    public class DbInitalizer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            ExerciseGarage2LexiconContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ExerciseGarage2LexiconContext>();
            if (!context.ParkedVehicle.Any())
            {
                context.AddRange
                (
                        new ParkedVehicle { VehicleType = "Car", RegistrationNumber = "ABC123", Color = "Red", Brand = "Toyota", Model = "Corolla", NumberOfWheels = 4, ArrivalTime = DateTime.Now },
                        new ParkedVehicle { VehicleType = "Car", RegistrationNumber = "DEF456", Color = "Blue", Brand = "Honda", Model = "Civic", NumberOfWheels = 4, ArrivalTime = DateTime.Now },
                        new ParkedVehicle { VehicleType = "Car", RegistrationNumber = "GHI789", Color = "White", Brand = "Ford", Model = "F-150", NumberOfWheels = 4, ArrivalTime = DateTime.Now },
                        new ParkedVehicle { VehicleType = "Car", RegistrationNumber = "JKL012", Color = "Black", Brand = "Chevrolet", Model = "Silverado", NumberOfWheels = 4, ArrivalTime = DateTime.Now },
                        new ParkedVehicle { VehicleType = "Car", RegistrationNumber = "MNO345", Color = "Green", Brand = "BMW", Model = "3 Series", NumberOfWheels = 4, ArrivalTime = DateTime.Now }
                );
            }
            context.SaveChanges();
        }
    }
}
