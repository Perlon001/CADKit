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
using CADDatabaseServices = ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using CADDatabaseServices = Autodesk.AutoCAD.Geometry;
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

        public virtual Group ToGroup()
        {
            if (CorrectInserting())
            {
                if(jig.Converters != null)
                {
                    jig.Converters.ForEach(x => { entities = x.Convert(entities); });
                }
                entities.TransformBy(Matrix3d.Displacement(new Vector3d(jig.JigPointResult.X, jig.JigPointResult.Y, jig.JigPointResult.Z)));
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
