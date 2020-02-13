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
    public abstract class ValueProvider : IValueProvider
    {
        protected ElevationValue elevationValue;
        protected Point3d basePoint;

        public ElevationValue ElevationValue { get { return elevationValue; } }
        public Point3d BasePoint { get { return basePoint; } }
        public abstract void PrepareValue();


    }
}
