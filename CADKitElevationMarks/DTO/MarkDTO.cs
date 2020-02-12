using CADKitElevationMarks.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CADKitElevationMarks.DTO
{
    public struct MarkDTO
    {
        public int id;
        public DrawingStandards standard;
        public MarkTypes type;
        public Type markClass;
        public Type markJig;
        public Bitmap picture16;
        public Bitmap picture32;
    }
}
