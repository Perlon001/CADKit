using CADKit;
using CADKit.Contracts;
using CADKitElevationMarks.Contracts;
using System;

#if ZwCAD
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Models
{
    public abstract class ValueProvider
    {
        public ElevationValue ElevationValue { get; protected set; }
        public Point3d BasePoint { get; protected set; }
        public abstract void Init();


    }
}
