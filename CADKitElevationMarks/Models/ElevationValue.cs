using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ElevationValue(string _value)
        {
            Sign = "";
            Value = _value;
        }

        public override string ToString()
        {
            return Sign + Value;
        }

        public ElevationValue Parse()
        {
            var culture = new CultureInfo("en-US");

            return Parse(culture);
        }

        public ElevationValue Parse(CultureInfo _culture)
        {
            double numericValue = Double.Parse(Value.Replace(".",","), NumberStyles.Number, CultureInfo.CreateSpecificCulture("pl-PL"));
            Value = Math.Abs(numericValue).ToString("N3");
            Sign = numericValue > 0 ? "+" : (numericValue < 0 ? "-" : "%%p");

            return this;
        }
    }
}
