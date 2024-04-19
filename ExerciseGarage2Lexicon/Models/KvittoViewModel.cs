namespace ExerciseGarage2Lexicon.Models
{
    public class KvittoViewModel
    {
        public string RegistrationNumber { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
        public double TotalParkingTimeInMinutes { get; set; }
        public double Price { get; set; } // Assuming 1 minute = 1kr
    }

}
