using CADKitElevationMarks.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Models
{
    public struct ElevationMarkItem 
    {
        public Type ElevationMarkType;
        public MarkTypes Kind;
        public Bitmap Icon;
    }
}
