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
            double result = 0;

            if (unitName == "Pounds")
            {
                result = _unitConverter.PoundsToKilograms(doubleValue);
            }
            if (unitName == "Stone")
            {
                result = _unitConverter.StoneToKilograms(doubleValue);
            }

            return result.ToString();
        }

        public string ConvertToUserUnit(string unitName, string value)
        {
            double doubleValue = Double.Parse(value);
            double result = 0;

            if (unitName == "Pounds")
            {
                result = _unitConverter.KilogramsToPounds(doubleValue);
            }
            if (unitName == "Stone")
            {
                result = _unitConverter.KilogramsToStone(doubleValue);
            }

            return result.ToString();
        }
    }
}
