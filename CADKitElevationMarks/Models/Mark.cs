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
        protected ElevationValue value;

        protected Mark(ValueProvider _provider)
        {
            provider = _provider;
        }

        public string Index { get; protected set; }
        public Point3d BasePoint { get; protected set; }
        public Point3d OriginPoint { get; protected set; }
        public IEnumerable<Entity> Entities { get; protected set; }

        protected abstract IEnumerable<Entity> GetEntities();
        public abstract void SetAttributeValue(BlockReference blockReference);

        public void Build()
        {
            provider.Init();
            value = provider.ElevationValue;
            BasePoint = provider.BasePoint;
            Entities = GetEntities();
            OriginPoint = default;
            Index = default;
        }
    }
}
