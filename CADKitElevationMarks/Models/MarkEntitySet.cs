using CADKit.Models;
using CADKit.Proxy;
using CADKitElevationMarks.Events;
using System;
using System.Collections.Generic;
using CADKit;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
#endif

#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
#endif

namespace CADKitElevationMarks.Models
{
    public class MarkEntitiesSet : EntitiesSet
    {
        public string Suffix { get; private set; } = "";
        public MarkEntitiesSet(IEnumerable<Entity> _entities, JigMark _jig) : base(_entities, _jig)
        {
            _jig.ChangeMarkSuffix += ChangeMarkSuffix;
            Suffix = _jig.Suffix;
        }

        private void ChangeMarkSuffix(object _sender, ChangeMarkSuffixEventArgs _e)
        {
            Suffix = _e.Suffix;
        }

        public BlockTableRecord ToBlock(string _prefix, string _index)
        {
            if (CADProxy.Editor.Drag(jig).Status == PromptStatus.OK)
            {
                var blockName = _prefix + Suffix + _index;
                var blockDef = base.ToBlock(blockName, new Point3d(0, 0, 0));
                InsertMarkBlock(blockDef, jig.JigPointResult);

                return blockDef;
            }

            throw new OperationCanceledException("*cancel*");
        }

        private void InsertMarkBlock(BlockTableRecord blockTableRecord, Point3d insertPoint)
        {
            using (var transaction = CADProxy.Document.TransactionManager.StartTransaction())
            {
                var space = CADProxy.Database.CurrentSpaceId.GetObject(OpenMode.ForWrite) as BlockTableRecord;
                using (var blockReference = new BlockReference(insertPoint, blockTableRecord.ObjectId))
                {
                    blockReference.ScaleFactors = new Scale3d(AppSettings.Get.ScaleFactor);
                    space.AppendEntity(blockReference);
                    transaction.AddNewlyCreatedDBObject(blockReference, true);
                    // SetAttributeValue(blockReference);
                    foreach (ObjectId id in blockReference.AttributeCollection)
                    {
                        transaction.AddNewlyCreatedDBObject(id.GetObject(OpenMode.ForRead), true);
                    }
                }
                transaction.Commit();
            }
        }

    }
}
