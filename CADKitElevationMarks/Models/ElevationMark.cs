using System;
using System.Collections.Generic;
using CADProxy;
using CADProxy.Internal;
using CADKit;
using CADKit.Models;
using CADKit.Services;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using System.Globalization;
using CADProxy.Extensions;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.EditorInput;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
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

        public abstract DrawingStandards DrawingStandard { get; }
        public abstract MarkTypes MarkType { get; }

        protected abstract JigMark GetMarkJig();

        protected abstract void SetAttributeValue(BlockReference blockReference);

        public abstract void CreateEntityList();

        public virtual void Create(EntitiesSet _entitiesSet)
        {
            var variables = SystemVariableService.GetActualSystemVariables();
            try
            {
                var promptPointOptions = new PromptPointOptions("Wskaż punkt wysokościowy:");
                basePoint = ProxyCAD.Editor.GetPoint(promptPointOptions);
                if (basePoint.Status == PromptStatus.OK)
                {
                    value = new ElevationValue(GetElevationSign(), GetElevationValue()).Parse(new CultureInfo("pl-PL"));
                    PersistEntities(_entitiesSet);
                }
            }
            catch (Exception ex)
            {
                ProxyCAD.Editor.WriteMessage(ex.Message);
            }
            finally
            {
                SystemVariableService.RestoreSystemVariables(variables);
                Utils.PostCommandPrompt();
            }
        }

        protected void PersistEntities(EntitiesSet _entitiesSet)
        {
            using (ProxyCAD.Document.LockDocument())
            {
                CreateEntityList();
                var jig = GetMarkJig();
                var result = ProxyCAD.Editor.Drag(jig);
                if (result.Status == PromptStatus.OK)
                {
                    switch (_entitiesSet)
                    {
                        case EntitiesSet.Group:
                            var entities = jig.GetEntity();
                            entities.TransformBy(Matrix3d.Displacement(new Point3d(0, 0, 0).GetVectorTo(jig.JigPointResult)));
                            entities.ToGroup();
                            break;
                        case EntitiesSet.Block:
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
            using (var transaction = ProxyCAD.Document.TransactionManager.StartTransaction())
            {
                var space = ProxyCAD.Database.CurrentSpaceId.GetObject(OpenMode.ForWrite) as BlockTableRecord;
                using (var blockReference = new BlockReference(insertPoint, blockTableRecord.ObjectId))
                {
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
            switch (AppSettings.Instance.DrawingUnit)
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
