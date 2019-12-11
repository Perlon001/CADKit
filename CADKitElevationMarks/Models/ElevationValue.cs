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
        private string sign;
        private double value;

        public ElevationValue(string _sign, double _value)
        {
            sign = _sign;
            value = _value;
        }

        public string Sign { get { return sign; } }
        public string Value { get { return value.ToString("0.000", CultureInfo.GetCultureInfo("pl-PL")); } }

    }
}
