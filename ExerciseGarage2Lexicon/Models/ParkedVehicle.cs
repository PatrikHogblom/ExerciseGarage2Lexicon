namespace ExerciseGarage2Lexicon.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }
        public string VehicleType { get; set; }
        public string RegistrationNumber { get; set; } // måste vara olika "ingen får vara samma som den andra"
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int NumberOfWheels { get; set; } // får inte vara negativt nummer

        public DateTime ArrivalTime { get; set; } // ska inte gå att editera eller manipulera
    }
}
