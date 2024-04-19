namespace ExerciseGarage2Lexicon
{
    public static class VehicleHelper
    {
        public static IEnumerable<string> GetVehicleTypes()
        {
            return new List<string> { "Airplane", "Bus", "Boat", "Car", "Motorcycle" };
        }
    }
}
