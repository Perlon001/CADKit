using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Models;
using System;
using System.Drawing;

namespace CADKitElevationMarks.DTO
{
    public struct MarkDTO
    {
        public int id;
        public DrawingStandards standard;
        public MarkTypes type;
        public Type markType;
        public Bitmap picture16;
        public Bitmap picture32;
        public JigMark jig;
    }
}
