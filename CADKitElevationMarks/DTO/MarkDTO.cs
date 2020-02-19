using CADKitElevationMarks.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CADKitElevationMarks.Models;

namespace CADKitElevationMarks.DTO
{
    public struct MarkDTO
    {
        public int id;
        public DrawingStandards standard;
        public MarkTypes type;
        public Type markType;
        public Type jigType;
        public Bitmap picture16;
        public Bitmap picture32;
        public JigMark jig;
    }
}
