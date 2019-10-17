namespace WeightTracker.Api.Helpers
{
    public class UnitConverter
    {
        // https://www.rapidtables.com/convert/weight/pound-to-stone.html
        // https://www.thecalculatorsite.com/conversions/common/kg-to-stones-pounds.php
        private const double PoundsToKilogramsValue = 2.2046226218;
        private const double KilogramsToPoundsValue = 0.45359237;
        private const double PoundsToStoneValue = 14;
        private const double StoneToPoundsValue = 0.0714286;
        private const double StoneToKilogramsValue = 6.35029318;

        public double PoundsToKilograms(double pounds)
        {
            return pounds / KilogramsToPoundsValue;
        }

        public double KilogramsToPounds(double kilograms)
        {
            return kilograms * PoundsToKilogramsValue;
        }

        public double PoundsToStone(double pounds)
        {
            return pounds / PoundsToStoneValue;
        }

        public double StoneToPounds(double stone)
        {
            return stone * StoneToPoundsValue;
        }

        public double StoneToKilograms(double stone)
        {
            return stone * StoneToKilogramsValue;
        }

        public double KilogramsToStone(double kilograms)
        {
            return kilograms / StoneToKilogramsValue;
        }
    }
}
