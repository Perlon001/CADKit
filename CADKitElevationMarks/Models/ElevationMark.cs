using System;
using System.Collections.Generic;
using CADKit;
using CADKit.Internal;
using CADKitBasic.Services;
using CADKitElevationMarks.Contracts;
using System.Globalization;
using CADKit.Extensions;
using CADKit.Models;
using CADKit.Proxy;
using CADKit.Contracts;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
#endif

namespace CADKitElevationMarks.Models
{
    public abstract class ElevationMark : IElevationMark
    {
        protected PromptPointResult basePoint;
        protected ElevationValue value;
        protected IEnumerable<Entity> entityList;
        protected string index = "";
        protected string blockName;
        protected bool isMarkCreateRunning = false;
        private OutputSet output;


        public abstract DrawingStandards DrawingStandard { get; }
        public abstract MarkTypes MarkType { get; }

        protected abstract JigMark GetMarkJig();

        protected abstract void SetAttributeValue(BlockReference blockReference);

        public abstract void CreateEntityList();

        protected virtual void CreateMark()
        {
            var variables = SystemVariableService.GetActualSystemVariables();
            try
            {
                var promptPointOptions = new PromptPointOptions("Wskaż punkt wysokościowy:");
                basePoint = CADProxy.Editor.GetPoint(promptPointOptions);
                if (basePoint.Status == PromptStatus.OK)
                {
                    value = new ElevationValue(GetElevationSign(), GetElevationValue()).Parse(new CultureInfo("pl-PL"));
                    PersistEntities();
                }
            }
            catch (Exception ex)
            {
                CADProxy.Editor.WriteMessage(ex.Message);
            }
            finally
            {
                SystemVariableService.RestoreSystemVariables(variables);
                Utils.PostCommandPrompt();
            }
        }

        public void Create(OutputSet _entitiesSet)
        {
            output = _entitiesSet;
            // TODO: mayby check actual environment settings
            var cmdActive = Convert.ToInt32(CADProxy.GetSystemVariable("CMDACTIVE"));
            if(cmdActive > 0)
            {
                isMarkCreateRunning = true;
                CADProxy.Document.CommandCancelled += CommandCancelled;
                CADProxy.CancelRunningCommand();
            }
            else
            {
                Application.MainWindow.Focus();
                CreateMark();
            }
        }

        private void CommandCancelled(object sender, CommandEventArgs e)
        {
            if (isMarkCreateRunning)
            {
                isMarkCreateRunning = false;
                CADProxy.Document.CommandCancelled -= CommandCancelled;
                Application.MainWindow.Focus();
                CreateMark();
            }
        }

        protected void PersistEntities()
        {
            using (CADProxy.Document.LockDocument())
            {
                CreateEntityList();
                var jig = GetMarkJig();
                var result = CADProxy.Editor.Drag(jig);
                if (result.Status == PromptStatus.OK)
                {
                    switch (output)
                    {
                        case OutputSet.group:
                            var entities = jig.GetEntity();
                            entities.TransformBy(Matrix3d.Scaling(AppSettings.Get.ScaleFactor, new Point3d(0, 0, 0)));
                            entities.TransformBy(Matrix3d.Displacement(new Point3d(0, 0, 0).GetVectorTo(jig.JigPointResult)));
                            entities.ToGroup();
                            break;
                        case OutputSet.block:
                            blockName = GetPrefix() + jig.GetSuffix() + index;
                            var defBlock = jig.GetEntity().ToBlock(blockName, new Point3d(0, 0, 0));
                            InsertMarkBlock(defBlock, jig.JigPointResult);
                            break;
                        default:
                            throw new NotSupportedException("Nie obsługiwany typ zbioru elementów");
                    }
                }
                Utils.FlushGraphics();
            }
        }

        protected void InsertMarkBlock(BlockTableRecord blockTableRecord, Point3d insertPoint)
        {
            using (var transaction = CADProxy.Document.TransactionManager.StartTransaction())
            {
                var space = CADProxy.Database.CurrentSpaceId.GetObject(OpenMode.ForWrite) as BlockTableRecord;
                using (var blockReference = new BlockReference(insertPoint, blockTableRecord.ObjectId))
                {
                    blockReference.ScaleFactors = new Scale3d(AppSettings.Get.ScaleFactor);
                    space.AppendEntity(blockReference);
                    transaction.AddNewlyCreatedDBObject(blockReference, true);
                    SetAttributeValue(blockReference);
                    foreach (ObjectId id in blockReference.AttributeCollection)
                    {
                        transaction.AddNewlyCreatedDBObject(id.GetObject(OpenMode.ForRead), true);
                    }
                }
                transaction.Commit();
            }
        }

        #region private methods
        private string GetPrefix()
        {
            return "ElevMark" + MarkType.ToString() + DrawingStandard.ToString();
        }

        private string GetElevationValue()
        {
            return Math.Round(Math.Abs(basePoint.Value.Y) * GetElevationFactor(), 3).ToString("0.000");
        }

        private string GetElevationSign()
        {
            if (Math.Round(Math.Abs(basePoint.Value.Y) * GetElevationFactor(), 3) == 0)
            {
                return "%%p";
            }
            else if (basePoint.Value.Y < 0)
            {
                return "-";
            }
            else
            {
                return "+";
            }
        }

        private double GetElevationFactor()
        {
            switch (AppSettings.Get.DrawingUnit)
            {
                case Units.m:
                    return 1;
                case Units.cm:
                    return 0.01;
                case Units.mm:
                    return 0.001;
                default:
                    throw new Exception("\nNie rozpoznana jednostka rysunkowa");
            }
        }
        #endregion
    }
}
