using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.DTO
{
    public class MarkComponentPropertyDTO
    {
        public string Name { get; private set; }
        public Dictionary<string, object> Property { get; private set; }
    }
}
