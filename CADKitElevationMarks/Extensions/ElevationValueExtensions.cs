using CADKitElevationMarks.Models;
using System;
using System.Globalization;

namespace CADKitElevationMarks.Extensions
{
    public static class ElevationValueExtensions
    {
        public static ElevationValue Parse(this ElevationValue _elevationValue)
        {
            var culture = new CultureInfo("en-US");

            return Parse(_elevationValue, culture);
        }

        public static ElevationValue Parse(this ElevationValue _elevationValue, CultureInfo _culture)
        {
            double numericValue;
            Double.TryParse(_elevationValue.Value, NumberStyles.AllowDecimalPoint, _culture, out numericValue);
            _elevationValue.Value = Math.Abs(numericValue).ToString("N3");
            _elevationValue.Sign = numericValue > 0 ? "+" : (numericValue < 0 ? "-" : "%%p");

            return _elevationValue;
        }
    }
}
