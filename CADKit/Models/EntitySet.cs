using CADKit.Contracts;
using CADKit.Extensions;
using CADKit.Proxy;
using System;
using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKit.Models
{
    public class EntitiesSet : IEntitiesSet
    {
        protected readonly Point3d originPoint;
        protected readonly EntittiesJig jig;
        private IEnumerable<Entity> entities;

        public EntitiesSet(IEnumerable<Entity> _entities, EntittiesJig _jig, Point3d _originPoint = default)
        {
            entities = _entities;
            originPoint = _originPoint;
            jig = _jig;
        }

        public virtual Group ToGroup()
        {
            var promptStatus = CADProxy.Editor.Drag(jig).Status;
            switch (promptStatus)
            {
                case PromptStatus.OK:
                    entities = jig.GetEntity();
                    jig.Transforms.ForEach(x => entities.TransformBy(x));
                    entities.TransformBy(Matrix3d.Displacement(originPoint.GetVectorTo(jig.JigPointResult)));
                    if (jig.Converters != null)
                    {
                        jig.Converters.ForEach(x => { entities = x.Convert(entities); });
                    }
                    return entities.ToGroup();
                case PromptStatus.Cancel:
                    throw new OperationCanceledException();
                default:
                    throw new Exception("Nie rozpoznany PrompStatus");
            }
        }

        public virtual BlockTableRecord ToBlock(string _name)
        {
            entities = jig.GetEntity();
            return entities.ToBlock(_name, originPoint);
        }

        protected virtual BlockReference InsertMarkBlock(BlockTableRecord blockTableRecord, Point3d insertPoint)
        {
            BlockReference blockReference = new BlockReference(insertPoint, blockTableRecord.ObjectId);
            using (var transaction = CADProxy.Document.TransactionManager.StartTransaction())
            {
                var space = CADProxy.Database.CurrentSpaceId.GetObject(OpenMode.ForWrite) as BlockTableRecord;
                blockReference.ScaleFactors = new Scale3d(AppSettings.Get.ScaleFactor);
                space.AppendEntity(blockReference);
                transaction.AddNewlyCreatedDBObject(blockReference, true);
                transaction.Commit();
            }

            return blockReference;
        }

    }
}
