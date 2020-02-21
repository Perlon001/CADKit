using CADKit.Contracts;
using CADKit.Models;
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
    public abstract class Mark : EntityComposite, IEntityComposite
    {
        private readonly ValueProvider provider;
        protected ElevationValue value;
        protected ICollection<IEntityConverter> converters;
        protected IEnumerable<Entity> entities;
        protected Point3d originPoint;
        protected Point3d basePoint;

        protected Mark(string _name, ValueProvider _provider) : base(_name)
        {
            provider = _provider;
            converters = new List<IEntityConverter>();
        }

        public string Index { get; protected set; }

        protected abstract IEnumerable<Entity> GetEntities();
        protected abstract JigMark GetJig();
        public abstract void SetAttributeValue(BlockReference blockReference);

        public Mark AddConverter(IEntityConverter _converter)
        {
            converters.Add(_converter);
            return this;
        }
        public void Build()
        {
            provider.Init();
            value = provider.ElevationValue;
            basePoint = provider.BasePoint;
            originPoint = default;
            Index = default;
            entities = GetEntities();
        }

        public MarkEntitiesSet GetEntitiesSet()
        {
            return new EntitiesSetBuilder<MarkEntitiesSet>(entities)
                .SetBasePoint(basePoint)
                .SetJig(GetJig())
                .Build();
        }
    }
}
