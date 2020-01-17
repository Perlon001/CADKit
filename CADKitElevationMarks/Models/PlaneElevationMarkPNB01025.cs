using CADKit;
using CADKit.Models;
using CADKit.Services;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADProxy;
using CADProxy.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using CADKitElevationMarks.Extensions;
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
    public class PlaneElevationMarkPNB01025 : ElevationMark, IElevationMark
    {
        public PlaneElevationMarkPNB01025() : base() { }

        public override DrawingStandards DrawingStandard { get { return DrawingStandards.PNB01025; } }

        public override MarkTypes MarkType { get { return MarkTypes.area; } }

        public override void Create(EntitiesSet _entitiesSet)
        {
            var variables = SystemVariableService.GetActualSystemVariables();
            try
            {
                var promptOptions = new PromptStringOptions("\nRzędna wysokościowa obszaru:");
                var textValue = ProxyCAD.Editor.GetString(promptOptions);
                if (textValue.Status == PromptStatus.OK)
                {
                    value = new ElevationValue("", textValue.StringResult).Parse();
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
                                    break;
                                case EntitiesSet.Block:
                                    blockName = GetBlockName() + jig.GetSuffix() + index;
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

        public override void CreateEntityList()
        {
            var en = new List<Entity>();

            var txt1 = new AttributeDefinition();
            txt1.SetDatabaseDefaults();
            txt1.TextStyle = ProxyCAD.Database.Textstyle;
            txt1.HorizontalMode = TextHorizontalMode.TextLeft;
            txt1.VerticalMode = TextVerticalMode.TextVerticalMid;
            txt1.ColorIndex = 7;
            txt1.Height = 2;
            txt1.AlignmentPoint = new Point3d(2, 1.5, 0);
            txt1.Tag = "Value";
            txt1.Prompt = "Value"; 
            txt1.TextString = this.value.Sign + this.value.Value;
            en.Add(txt1);

            var l1 = new Line(new Point3d(-1.5, -1.5, 0), new Point3d(1.5, 1.5, 0));
            en.Add(l1);

            var l2 = new Line(new Point3d(-1.5, 1.5, 0), new Point3d(1.5, -1.5, 0));
            en.Add(l2);

            var textArea = ProxyCAD.GetTextArea(txt1);
            var l3 = new Line(new Point3d(0, 0, 0), new Point3d(textArea[1].X - textArea[0].X + 2, 0, 0));
            en.Add(l3);

            this.entityList = en;
        }

        protected override EntityListJig GetMarkJig()
        {
            return new JigMark(entityList, basePoint.Value, new AttributeToDBTextConverter());
        }

        protected override void SetAttributeValue(BlockReference blockReference)
        {
            using (var blockTableRecord = blockReference.BlockTableRecord.GetObject(OpenMode.ForRead) as BlockTableRecord)
            {
                var attDef = blockTableRecord.GetAttribDefinition("Value");
                if (!attDef.Constant)
                {
                    var attRef = new AttributeReference();
                    attRef.SetAttributeFromBlock(attDef, blockReference.BlockTransform);
                    attRef.TextString = value.Sign + value.Value;
                    blockReference.AttributeCollection.AppendAttribute(attRef);
                }
            }
        }
    }
}
