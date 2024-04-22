namespace ExerciseGarage2Lexicon.Services
{
    public interface IPriceService
    {
        double GetPrice();
        void SetPrice(double price);
    }

    public class PriceService : IPriceService
    {
        private double _price;
        public double GetPrice()
        {
            return _price;
        }

        public void SetPrice(double price)
        {
            _price = price;
        }
    }
}
