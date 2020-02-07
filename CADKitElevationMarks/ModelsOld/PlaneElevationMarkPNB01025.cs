using System;
using System.Collections.Generic;
using CADKit.Extensions;
using CADKit.Proxy;
using CADKit.Utils;
using CADKitBasic.Services;
using CADKitElevationMarks.Contracts;
using CADKit.Internal;
using CADKit.Contracts;

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
        public override DrawingStandards DrawingStandard { get { return DrawingStandards.PNB01025; } }

        public override MarkTypes MarkType { get { return MarkTypes.area; } }

        public override IEnumerable<Entity> Build()
        {
            CreateEntityList();
            return entityList;
        }

        protected override void CreateMark()
        {
            var variables = SystemVariableService.GetActualSystemVariables();
            try
            {
                var promptOptions = new PromptStringOptions("\nRzędna wysokościowa obszaru:");
                var textValue = CADProxy.Editor.GetString(promptOptions);
                if (textValue.Status == PromptStatus.OK)
                {
                    value = new ElevationValue(textValue.StringResult).Parse();
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

        public override void CreateEntityList()
        {
            var en = new List<Entity>();

            var txt1 = new AttributeDefinition();
            txt1.SetDatabaseDefaults();
            txt1.TextStyle = CADProxy.Database.Textstyle;
            txt1.HorizontalMode = TextHorizontalMode.TextLeft;
            txt1.VerticalMode = TextVerticalMode.TextVerticalMid;
            txt1.ColorIndex = 7;
            txt1.Height = 2;
            txt1.Position = new Point3d(2, 1.5, 0);
            txt1.Justify = AttachmentPoint.MiddleLeft;
            txt1.AlignmentPoint = new Point3d(2, 1.5, 0);
            txt1.Tag = "Value";
            txt1.Prompt = "Value";
            txt1.TextString = value.ToString();
            en.Add(txt1);

            var l1 = new Line(new Point3d(-1.5, -1.5, 0), new Point3d(1.5, 1.5, 0));
            en.Add(l1);

            var l2 = new Line(new Point3d(-1.5, 1.5, 0), new Point3d(1.5, -1.5, 0));
            en.Add(l2);

            var textArea = CADProxy.GetTextArea(CADProxy.ToDBText(txt1));
            var l3 = new Line(new Point3d(0, 0, 0), new Point3d(textArea[1].X - textArea[0].X + 2, 0, 0));
            en.Add(l3);

            entityList = en;
        }

        protected override JigMark GetMarkJig()
        {
            return new JigMark(entityList, new Point3d(0, 0, 0), new List<IEntityConverter>() { new AttributeToDBTextConverter() });
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
