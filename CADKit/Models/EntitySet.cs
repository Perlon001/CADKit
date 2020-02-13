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
        protected EntittiesJig jig;
        private IEnumerable<Entity> entities;

        public EntitiesSet(IEnumerable<Entity> _entities, EntittiesJig _jig)
        {
            entities = _entities;
            jig = _jig;
        }

        public virtual Group ToGroup()
        {
            if (CorrectInserting())
            {
                entities = jig.GetEntity();
                jig.Transforms.ForEach(x => entities.TransformBy(x));
                entities.TransformBy(Matrix3d.Displacement(new CADGeometry.Point3d(0,0,0).GetVectorTo(jig.JigPointResult)));
                if (jig.Converters != null)
                {
                    jig.Converters.ForEach(x => { entities = x.Convert(entities); });
                }
                return entities.ToGroup();
            }

            throw new OperationCanceledException("*cancel*");
        }

        public virtual BlockTableRecord ToBlock(string _name, CADGeometry.Point3d _origin)
        {
            if (CorrectInserting())
            {
                entities = jig.GetEntity();
                return entities.ToBlock(_name, _origin);
            }

            throw new OperationCanceledException("*cancel*");
        }

        private bool CorrectInserting()
        {
            var result = CADProxy.Editor.Drag(jig).Status == PromptStatus.OK;

            return result;
        }
    }
}
