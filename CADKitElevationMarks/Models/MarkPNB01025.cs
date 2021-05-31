using CADKit.Contracts;
using CADKit.Extensions;
using CADKit.Models;
using CADKit.Proxy;
using CADKit.Utils;
using System.Collections.Generic;
using System.Linq;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.Colors;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Colors;
#endif

namespace CADKitElevationMarks.Models
{
    public class MarkPNB01025 : Mark, IEntityComposite
    {
        public MarkPNB01025(ElevationValueProvider _provider) : base("", _provider)
        {
            Title = "Kota wysokościowa w przekroju wg PNB01025";
        }

        public override void SetAttributeValue(BlockReference blockReference)
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

        protected override void BuildComponents()
        {
            var component = new MarkComponent("Value")
            {
                Title = "Wartość rzędnej wysokościowej"
            };
            //component.AddComponent(new EntityProperty("Layer", "0"));
            //component.AddComponent(new EntityProperty("Linetype", "BYLAYER"));
            //component.AddComponent(new EntityProperty("Color", "BYLAYER"));
            //component.AddComponent(new EntityProperty("TextStyle", "Standard"));
            component.Properties.Add("Layer", "0");
            component.Properties.Add("Linetype", "BYLAYER");
            component.Properties.Add("Color", "BYLAYER");
            component.Properties.Add("TextStyle", "Standard");
            component.Tag = component.Properties;

            AddComponent(component);

            component = new MarkComponent("Leader")
            {
                Title = "Linia odnośnika"
            };
            //component.AddComponent(new EntityProperty("Layer", "0"));
            //component.AddComponent(new EntityProperty("Linetype", "BYLAYER"));
            //component.AddComponent(new EntityProperty("Color", "BYLAYER"));
            AddComponent(component);

            component = new MarkComponent("Arrowhead")
            {
                Title = "Grot odnośnika"
            };
            //component.AddComponent(new EntityProperty("Layer", "0"));
            //component.AddComponent(new EntityProperty("Linetype", "BYLAYER"));
            //component.AddComponent(new EntityProperty("Color", "BYLAYER"));
            AddComponent(component);

            component = new MarkComponent("Arrowhatch")
            {
                Title = "Wypełnienie grota"
            };
            //component.AddComponent(new EntityProperty("Layer", "0"));
            //component.AddComponent(new EntityProperty("Linetype", "BYLAYER"));
            //component.AddComponent(new EntityProperty("Color", "BYLAYER"));
            AddComponent(component);
        }

        protected override void SetComponentsEntity()
        {
            SetValueComponent();
            SetArrowheadComponent();
            SetLeaderComponent();
        }

        protected override JigMark GetJig()
        {
            var conv = new List<IEntityConverter>
            {
                new AttributeToDBTextConverter()
            };
            return new JigVerticalConstantVerticalAndHorizontalMirrorMark(Entities, originPoint, basePoint, conv);
        }

        private void SetValueComponent()
        {
            var att1 = new AttributeDefinition();
            att1.SetDatabaseDefaults();
            att1.TextStyle = CADProxy.Database.Textstyle;
            att1.HorizontalMode = TextHorizontalMode.TextLeft;
            att1.VerticalMode = TextVerticalMode.TextVerticalMid;
            att1.ColorIndex = 7;
            att1.Height = 2;
            att1.Position = new Point3d(0, 4.5, 0);
            att1.Justify = AttachmentPoint.MiddleLeft;
            att1.AlignmentPoint = new Point3d(0, 4.5, 0);
            att1.Tag = "Value";
            att1.Prompt = "Value";
            att1.TextString = value.ToString();

            (components.First(x => x.Name == "Value") as MarkComponent).Entity = att1;
        }

        private void SetArrowheadComponent()
        {

            var pl1 = new Polyline();
            pl1.AddVertexAt(0, new Point2d(-1.5, 1.5), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(1.5, 1.5), 0, 0, 0);
            if (value.Value == "0,000")
            {
                pl1.Closed = true;
                AddHatchingArrow();
                Index = "zero";
            }
            (components.First(x => x.Name == "Arrowhead") as MarkComponent).Entity = pl1;
        }

        private void SetLeaderComponent()
        {
            var textArea = CADProxy.GetTextArea(CADProxy.ToDBText((components.First(x => x.Name == "Value") as MarkComponent).Entity as AttributeDefinition));
            var pl2 = new Polyline();
            pl2.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl2.AddVertexAt(0, new Point2d(0, 3), 0, 0, 0);
            pl2.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X, 3), 0, 0, 0);
            (components.First(x => x.Name == "Leader") as MarkComponent).Entity = pl2;
        }
        
        private void AddHatchingArrow()
        {
            using (var transaction = CADProxy.Database.TransactionManager.StartTransaction())
            {
                var polyline = new Polyline();
                polyline.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                polyline.AddVertexAt(0, new Point2d(1.5, 1.5), 0, 0, 0);
                polyline.AddVertexAt(0, new Point2d(0, 1.5), 0, 0, 0);
                polyline.Closed = true;

                var blockTableRecord = transaction.GetObject(CADProxy.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                var objIds = new ObjectIdCollection()
                {
                    blockTableRecord.AppendEntity(polyline)
                };
                transaction.AddNewlyCreatedDBObject(polyline, true);

                var hatch = new Hatch();
                hatch.SetDatabaseDefaults();
                hatch.SetHatchPattern(HatchPatternType.PreDefined, "SOLID");
                hatch.Associative = false;
                hatch.AppendLoop((int)HatchLoopTypes.Default, objIds);
                hatch.EvaluateHatch(true);

                polyline.Erase();
                (components.First(x => x.Name == "Arrowhatch") as MarkComponent).Entity = hatch;
            }
        }
    }
}
