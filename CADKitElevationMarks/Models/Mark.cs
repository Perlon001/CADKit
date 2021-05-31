using CADKit.Contracts;
using CADKit.Models;
using System.Collections.Generic;
using System.Linq;

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
    public abstract class Mark : EntityComposite
    {
        private readonly ValueProvider provider;
        private JigMark jig;

        protected ElevationValue value;
        protected ICollection<IEntityConverter> converters;
        protected Point3d originPoint;
        protected Point3d basePoint;

        // public ICollection<IComponent> Components { get; private set; }

        protected IEnumerable<Entity> Entities
        {
            get
            {
                return components
                    .Where(x => (x as MarkComponent).Entity != null)
                    .Select(m => (m as MarkComponent).Entity);
            }
        }


        protected Mark(string _name, ValueProvider _provider) : base(_name)
        {
            provider = _provider;
            converters = new List<IEntityConverter>();
            Properties.Add("Layer", "0");
            Properties.Add("Linetype", "BYLAYER");
            Properties.Add("Color", "BYBLOCK");
            // Components = new List<IComponent>();
            BuildComponents();
        }

        public string Index { get; protected set; }

        protected abstract void SetComponentsEntity();
        protected abstract void BuildComponents();
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
            SetComponentsEntity();
            jig = GetJig();
        }

        public MarkEntitiesSet GetEntitiesSet()
        {
            return new EntitiesSetBuilder<MarkEntitiesSet>(Entities)
                .SetBasePoint(basePoint)
                .SetJig(jig)
                .Build();
        }
    }
}
