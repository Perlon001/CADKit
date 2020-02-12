using CADKit.Contracts;
using CADKit.Extensions;
using CADKit.Geometry;
using CADKit.Proxy;
using System;
using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using CADGeometry = ZwSoft.ZwCAD.Geometry;
using CADDatabaseServices = ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using CADDatabaseServices = Autodesk.AutoCAD.Geometry;
using CADGeometry = Autodesk.AutoCAD.Geometry;
#endif

namespace CADKit.Models
{
    public class EntitiesSet : IEntitiesSet
    {
        private EntittiesJig jig;
        private IEnumerable<Entity> entities;

        public EntitiesSet(IEnumerable<Entity> _entities, EntittiesJig _jig)
        {
            entities = _entities;
            jig = _jig;
        }

        public virtual BlockTableRecord ToBlock()
        {
            throw new NotImplementedException();
        }

        public IEntitiesSet Transform(Matrix3d[] _transforms)
        {
            if (_transforms != null)
            {
                foreach (var transform in _transforms)
                {
                    Transform(transform);
                }
            }
            return this;
        }

        public IEntitiesSet Transform(Matrix3d _transform)
        {
            if (_transform != null)
            {
                entities.TransformBy(_transform);
            }
            return this;
        }

        public virtual Group ToGroup()
        {
            //TODO: z jiga trzeba wziąć bufor, najpierw go wystawic jako publiczna property
            if (CorrectInserting())
            {
                entities = jig.GetEntity();

                //entities.TransformBy(Matrix3d.Displacement(new Vector3d(jig.JigPointResult.X, jig.JigPointResult.Y, jig.JigPointResult.Z)));
                //entities.TransformBy(Matrix3d.Displacement(new CADGeometry.Point3d(0,0,0).GetVectorTo(jig.JigPointResult)));
                if (jig.Converters != null)
                {
                    jig.Converters.ForEach(x => { entities = x.Convert(entities); });
                }
                return entities.ToGroup();
            }

            throw new OperationCanceledException("*cancel*");
        }

        protected bool CorrectInserting()
        {
            var result = CADProxy.Editor.Drag(jig).Status == PromptStatus.OK;

            return result;
        }
    }
}
