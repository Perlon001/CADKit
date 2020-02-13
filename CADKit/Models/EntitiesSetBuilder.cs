using CADKit.Contracts;
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

namespace CADKit.Models
{
    public class EntitiesSetBuilder<T> where T : EntitiesSet
    {
        private IEnumerable<Entity> entities;
        private Point3d basePoint;
        private Type jigType;
        private IList<Type> converterTypes;
        private IList<Matrix3d> transforms;

        public EntitiesSetBuilder(IEnumerable<Entity> collection)
        {
            entities = collection;
            basePoint = new Point3d(0, 0, 0);
            jigType = typeof(EntittiesJig);
            converterTypes = new List<Type>();
            transforms = new List<Matrix3d>();
        }

        public EntitiesSetBuilder<T> SetBasePoint(Point3d _basePoint)
        {
            basePoint = _basePoint;
            return this;
        }

        public EntitiesSetBuilder<T> SetJig(Type _jigType)
        {
            jigType = _jigType;
            return this;
        }

        public EntitiesSetBuilder<T> AddTransforms(Matrix3d _matrix)
        {
            transforms.Add(_matrix);
            return this;
        }

        public EntitiesSetBuilder<T> AddConverter(Type _converterType)
        {
            converterTypes.Add(_converterType);
            return this;
        }

        public T Build()
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

            return Activator.CreateInstance(typeof(T), args) as T;
        }
    }
}
