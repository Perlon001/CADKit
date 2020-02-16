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
        public SetAttributeEventHandler SetAttributeHandler;
        public string Suffix { get; private set; } = "";
        public MarkEntitiesSet(IEnumerable<Entity> _entities, JigMark _jig, Point3d _origin = default) : base(_entities, _jig, _origin)
        {
            _jig.ChangeMarkSuffix += ChangeMarkSuffix;
            Suffix = _jig.Suffix;
        }

        private void ChangeMarkSuffix(object _sender, ChangeMarkSuffixEventArgs _e)
        {
            Suffix = _e.Suffix;
        }

        public override BlockTableRecord ToBlock(string _name)
        {
            var promtStatus = CADProxy.Editor.Drag(jig).Status;
            switch (promtStatus)
            {
                case PromptStatus.OK:
                    var blockName = _name + Suffix;
                    return base.ToBlock(blockName);
                case PromptStatus.Cancel:
                    throw new OperationCanceledException();
                default:
                    throw new Exception("Nie rozpoznany PromptStatus");
            }
        }


        public BlockReference ToBlockReference(string _name)
        {
            var promptStatus = CADProxy.Editor.Drag(jig).Status;
            switch (promptStatus)
            {
                case PromptStatus.OK:
                    var blockDef = base.ToBlock(_name + Suffix);
                    return InsertMarkBlock(blockDef, jig.JigPointResult);
                case PromptStatus.Cancel:
                    throw new OperationCanceledException();
                default:
                    throw new Exception("Nie rozpoznany PromptStatus");
            }
        }

        protected override BlockReference InsertMarkBlock(BlockTableRecord blockTableRecord, Point3d insertPoint)
        {
            BlockReference blockReference = new BlockReference(insertPoint, blockTableRecord.ObjectId);
            using (var transaction = CADProxy.Document.TransactionManager.StartTransaction())
            {
                var space = CADProxy.Database.CurrentSpaceId.GetObject(OpenMode.ForWrite) as BlockTableRecord;
                blockReference.ScaleFactors = new Scale3d(AppSettings.Get.ScaleFactor);
                space.AppendEntity(blockReference);
                transaction.AddNewlyCreatedDBObject(blockReference, true);
                SetAttributeHandler?.Invoke(blockReference);
                foreach (ObjectId id in blockReference.AttributeCollection)
                {
                    transaction.AddNewlyCreatedDBObject(id.GetObject(OpenMode.ForRead), true);
                }
                transaction.Commit();
            }

            return blockReference;
        }
    }
}
