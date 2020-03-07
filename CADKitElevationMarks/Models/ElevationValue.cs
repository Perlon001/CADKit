using CADKit.Proxy;
using System;
using System.Globalization;

namespace CADKitElevationMarks.Models
{
    public struct ElevationValue
    {
        public string Sign { get; set; }
        public string Value { get; set; }

        public ElevationValue(string _sign, string _value)
        {
            Sign = _sign;
            Value = _value;
        }

        public ElevationValue(string _value) : this("", _value) { }

        public override string ToString()
        {
            return Sign + Value;
        }

        public ElevationValue Parse()
        {
            var culture = new CultureInfo("pl-PL");

            return Parse(culture);
        }

        public ElevationValue Parse(CultureInfo _culture)
        {
            //TODO: CHeck CultureInfo and run specifi parser decimal point , or .
            try
            {
                double numericValue = Double.Parse(Value.Replace(".", ","), NumberStyles.Number, _culture);
                Value = Math.Abs(numericValue).ToString("N3");
                Sign = numericValue > 0 ? "+" : (numericValue < 0 ? "-" : "%%p");
            }
            catch (Exception ex)
            {
                CADProxy.Editor.WriteMessage(ex.Message);
            }

            return this;
        }
    }
}
