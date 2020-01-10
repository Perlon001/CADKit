using System;
using System.Collections.Generic;
using System.Linq;
using CADProxy;
using CADProxy.Internal;
using CADKit;
using CADKit.Models;
using CADKit.Services;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Extensions;
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

        protected abstract void CreateEntityList();
        
        protected abstract EntityListJig GetMarkJig(Group group, Point3d point);

        protected abstract void InsertMarkBlock(Point3d insertPoint);

        protected void GroupErase(Transaction tr, Group group)
        {
            foreach (var id in group.GetAllEntityIds())
            {
                if (!id.IsErased)
                {
                    tr.GetObject(id, OpenMode.ForWrite).Erase();
                }
            }
            group.Erase(true);
        }

        public abstract DrawingStandards DrawingStandard { get; }
        
        public abstract MarkTypes MarkType { get; }

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
                    using (ProxyCAD.Document.LockDocument())
                    {
                        CreateEntityList();
                        using (var tr = ProxyCAD.Document.TransactionManager.StartTransaction())
                        {
                            var jigGroup = entityList.Clone().ToGroup();
                            var jig = GetMarkJig(jigGroup, basePoint.Value);
                            (jigGroup.ObjectId.GetObject(OpenMode.ForWrite) as Group).SetVisibility(false);
                            var result = ProxyCAD.Editor.Drag(jig);
                            if (result.Status == PromptStatus.OK)
                            {
                                switch (_entitiesSet)
                                {
                                    case EntitiesSet.Group:
                                        jig.GetEntity().ToGroup();
                                        break;
                                    case EntitiesSet.Block:
                                        blockName = GetBlockName() + jig.GetSuffix() + index;
                                        entityList.ToBlock(blockName, new Point3d(0, 0, 0));
                                        InsertMarkBlock(jig.Origin);
                                        break;
                                    default:
                                        throw new NotSupportedException("Nie obsługiwany typ zbioru elementów");
                                }
                            }
                            GroupErase(tr, jigGroup);
                            Utils.FlushGraphics();
                            tr.Commit();
                        }
                    }
                }
            } 
            catch(Exception ex)
            {
                ProxyCAD.Editor.WriteMessage(ex.Message);
            }
            finally
            {
                SystemVariableService.RestoreSystemVariables(variables);
                Utils.PostCommandPrompt();
            }
        }

        #region private methods
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

        private string GetBlockName()
        {
            return "CK_EM_" + MarkType.ToString() + DrawingStandard.ToString();
        }
        #endregion

    }
}
