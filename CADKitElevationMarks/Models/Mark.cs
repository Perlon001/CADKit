using CADKit.Proxy;
using CADKitElevationMarks.Contracts;
using System;
using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Models
{
    public abstract class Mark : IMark
    {
        protected Mark(IValueProvider _provider)
        {
            _provider.PrepareValue();
            Value = _provider.ElevationValue;
            BasePoint = _provider.BasePoint;
            OriginPoint = default;
            Index = default;
        }

        public string Index { get; protected set; }
        public Point3d BasePoint { get; protected set; }
        public Point3d OriginPoint { get; protected set; }
        public ElevationValue Value { get; protected set; }

        public abstract IEnumerable<Entity> GetEntities();
        public abstract void SetAttributeValue(BlockReference blockReference);
    }
}
