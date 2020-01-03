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
    }
}
