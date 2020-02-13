using CADKitElevationMarks.Contracts;
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
        protected readonly ElevationValue value;

        protected Mark(IValueProvider _provider)
        {
            _provider.PrepareValue();
            value = _provider.ElevationValue;
            BasePoint = _provider.BasePoint;
            OriginPoint = default;
            Index = default;
        }

        public string Index { get; protected set; }
        public Point3d BasePoint { get; protected set; }
        public Point3d OriginPoint { get; protected set; }

        public abstract IEnumerable<Entity> GetEntities();
        public abstract void SetAttributeValue(BlockReference blockReference);
    }
}
