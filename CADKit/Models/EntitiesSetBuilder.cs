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
        private Point3d originPoint;
        private Point3d basePoint;
        private Type jigType;
        private EntittiesJig jig;
        private IList<Type> converterTypes;
        private IList<IEntityConverter> converters;
        private IList<Matrix3d> transforms;

        public EntitiesSetBuilder(IEnumerable<Entity> collection)
        {
            entities = collection;
            originPoint = new Point3d(0, 0, 0);
            basePoint = new Point3d(0, 0, 0);
            jigType = typeof(EntittiesJig);
            converterTypes = new List<Type>();
            converters = new List<IEntityConverter>();
            transforms = new List<Matrix3d>();
        }

        public EntitiesSetBuilder<T> SetOriginPoint(Point3d _originPoint)
        {
            originPoint = _originPoint;
            return this;
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

        public EntitiesSetBuilder<T> SetJig(EntittiesJig _jig)
        {
            jigType = null;
            jig = _jig;
            return this;
        }

        public EntitiesSetBuilder<T> AddTransforms(Matrix3d _matrix)
        {
            transforms.Add(_matrix);
            return this;
        }

        public EntitiesSetBuilder<T> AddConverterType(Type _converterType)
        {
            converterTypes.Add(_converterType);
            return this;
        }

        public EntitiesSetBuilder<T> AddConverter(IEntityConverter _converter)
        {
            converters.Add(_converter);
            return this;
        }

        public virtual T Build()
        {
            if (converterTypes != null)
            {
                foreach(var conv in converterTypes)
                {
                    converters.Add(Activator.CreateInstance(conv) as IEntityConverter);
                }
            }
            Object[] args;
            if (jig == null)
            {
                Object[] jigArgs = { entities, originPoint, basePoint, converters };
                var jig = Activator.CreateInstance(jigType, jigArgs);
                object[] arg = { entities, jig, originPoint };
                args = arg;
            }
            else
            {
                object[] arg = { entities, jig, originPoint };
                args = arg;
            }
            var result = Activator.CreateInstance(typeof(T), args) as T;
            return result;
        }
    }
}
