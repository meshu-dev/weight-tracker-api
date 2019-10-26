using System;

namespace WeightTracker.Api.Helpers
{
    public class UserUnitConverter
    {
        private readonly UnitConverter _unitConverter;

        public UserUnitConverter(UnitConverter unitConverter)
        {
            _unitConverter = unitConverter;
        }

        public string ConvertToBaseUnit(string unitName, string value)
        {
            double doubleValue = Double.Parse(value);

            if (unitName == "lbs")
            {
                doubleValue = _unitConverter.PoundsToKilograms(doubleValue);
            }
            if (unitName == "st")
            {
                doubleValue = _unitConverter.StoneToKilograms(doubleValue);
            }
            if (unitName == "st lbs")
            {
                string[] values = value.Split(".");
                double stone = Double.Parse(values[0]);
                double pounds = Double.Parse(values[1]);

                double stoneInKg = _unitConverter.StoneToKilograms(stone);
                double poundsInKg = _unitConverter.PoundsToKilograms(pounds);
                doubleValue = stoneInKg + poundsInKg;
            }
            return doubleValue.ToString();
        }

        public string ConvertToUserUnit(string unitName, string value)
        {
            double doubleValue = Double.Parse(value);

            if (unitName == "lbs")
            {
                doubleValue = _unitConverter.KilogramsToPounds(doubleValue);
            }
            if (unitName == "st")
            {
                doubleValue = _unitConverter.KilogramsToStone(doubleValue);
            }
            if (unitName == "st lbs")
            {
                int stone = (int) _unitConverter.KilogramsToStone(doubleValue);

                double pounds = _unitConverter.KilogramsToPounds(doubleValue) % 14;

                string[] units = value.Split(" ");
                return stone.ToString() + " " + units[0] + " " + pounds.ToString() + " " + units[1]; 
            }
            doubleValue = Math.Round(doubleValue, 2, MidpointRounding.AwayFromZero);
            return doubleValue.ToString() + " " + unitName;
        }
    }
}
