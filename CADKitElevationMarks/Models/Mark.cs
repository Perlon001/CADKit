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
    public abstract class Mark
    {
        private readonly ValueProvider provider;
        protected Mark(ValueProvider _provider)
        {
            provider = _provider;
        }

        public string Index { get; protected set; }
        public Point3d BasePoint { get; protected set; }
        public Point3d OriginPoint { get; protected set; }
        public ElevationValue Value { get; protected set; }

        public abstract IEnumerable<Entity> GetEntities();
        public abstract void SetAttributeValue(BlockReference blockReference);

        public void Init()
        {
            provider.Init();
            Value = provider.ElevationValue;
            BasePoint = provider.BasePoint;
            OriginPoint = default;
            Index = default;
        }
    }
}
