﻿using System;

namespace WeightTracker.Api.Helpers
{
    #pragma warning disable CS1591
    public class UserUnitConverter
    {
        private readonly UnitConverter _unitConverter;

        public UserUnitConverter(UnitConverter unitConverter)
        {
            _unitConverter = unitConverter;
        }

        public string ConvertToBaseUnit(string unitName, string value)
        {
            double doubleValue = 0;

            if (unitName == "lbs")
            {
                doubleValue = Double.Parse(value);
                doubleValue = _unitConverter.PoundsToKilograms(doubleValue);
            }
            if (unitName == "st")
            {
                doubleValue = Double.Parse(value);
                doubleValue = _unitConverter.StoneToKilograms(doubleValue);
            }
            if (unitName == "st lbs")
            {
                string[] values = value.Split(".");
                double stone = Double.Parse(values[0]);

                string poundsText = values[1];
                if (values[2] != null) poundsText += "." + values[2];

                double pounds = Double.Parse(poundsText);

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
                pounds = Math.Round(pounds, 2);

                return stone.ToString() + "." + pounds.ToString(); 
            }
            doubleValue = Math.Round(doubleValue, 2, MidpointRounding.AwayFromZero);
            return doubleValue.ToString();
        }
    }
    #pragma warning restore CS1591
}
