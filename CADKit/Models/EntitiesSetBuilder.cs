using CADKit.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKit.Models
{
    public class EntitiesSetBuilder
    {
        private IEnumerable<Entity> entities;
        private Point3d basePoint;
        private Type jigType;
        private IList<Type> converterTypes;

        public EntitiesSetBuilder(IEntityListBuilder builder) : this(builder.Build()) { }

        public EntitiesSetBuilder(IEnumerable<Entity> collection)
        {
            entities = collection;
            basePoint = new Point3d(0, 0, 0);
            jigType = typeof(EntittiesJig);
            converterTypes = new List<Type>();
        }

        public EntitiesSetBuilder SetBasePoint(Point3d _basePoint)
        {
            basePoint = _basePoint;
            return this;
        }

        public EntitiesSetBuilder AssignJig(Type _jigType)
        {
            jigType = _jigType;
            return this;
        }

        public EntitiesSetBuilder AddConverter(Type _converterType)
        {
            converterTypes.Add(_converterType);
            return this;
        }

        public EntitiesSet Build()
        {
            IList<IEntityConverter> converters = new List<IEntityConverter>();
            if (converterTypes != null)
            {
                foreach(var conv in converterTypes)
                {
                    converters.Add(Activator.CreateInstance(conv) as IEntityConverter);
                }
            }
            Object[] jigArgs = { entities, basePoint, converters };
            var jig = Activator.CreateInstance(jigType, jigArgs);
            Object[] args = { entities, jig };

            return Activator.CreateInstance(typeof(EntitiesSet), args) as EntitiesSet;
        }
    }
}
