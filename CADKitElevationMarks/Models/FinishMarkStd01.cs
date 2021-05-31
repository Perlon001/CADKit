using System.Collections.Generic;
using System.Linq;
using CADKit.Contracts;
using CADKit.Extensions;
using CADKit.Models;
using CADKit.Proxy;
using CADKit.Utils;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Models
{
    public class FinishMarkStd01 : Mark
    {
        public FinishMarkStd01(ElevationValueProvider _provider) : base("", _provider) { }

        public override void SetAttributeValue(BlockReference blockReference)
        {
            using (var blockTableRecord = blockReference.BlockTableRecord.GetObject(OpenMode.ForRead) as BlockTableRecord)
            {
                var attDef = blockTableRecord.GetAttribDefinition("Sign");
                if (!attDef.Constant)
                {
                    var attRef = new AttributeReference();
                    attRef.SetAttributeFromBlock(attDef, blockReference.BlockTransform);
                    attRef.TextString = value.Sign;
                    blockReference.AttributeCollection.AppendAttribute(attRef);
                }
                attDef = blockTableRecord.GetAttribDefinition("Value");
                if (!attDef.Constant)
                {
                    var attRef = new AttributeReference();
                    attRef.SetAttributeFromBlock(attDef, blockReference.BlockTransform);
                    attRef.TextString = value.Value;
                    blockReference.AttributeCollection.AppendAttribute(attRef);
                }
            }
        }

        protected override void BuildComponents()
        {
            MarkComponent component;
            component = new MarkComponent("Sign")
            {
                Title = "Znak rzędnej wysokościowej"
            };
            //component.AddComponent(new EntityProperty("Layer", "0"));
            //component.AddComponent(new EntityProperty("Linetype", "BYLAYER"));
            //component.AddComponent(new EntityProperty("Color", "BYLAYER"));
            //component.AddComponent(new EntityProperty("TextStyle", "Standard"));
            AddComponent(component);

            component = new MarkComponent("Value")
            {
                Title = "Wartość rzędnej wysokościowej"
            };
            //component.AddComponent(new EntityProperty("Layer", "0"));
            //component.AddComponent(new EntityProperty("Linetype", "BYLAYER"));
            //component.AddComponent(new EntityProperty("Color", "BYLAYER"));
            //component.AddComponent(new EntityProperty("TextStyle", "Standard"));
            //AddComponent(component);

            //component.Properties.Add("Layer", "0");
            //component.Properties.Add("Linetype", "BYLAYER");
            //component.Properties.Add("Color", "BYLAYER");
            //component.Properties.Add("TextStyle", "Standard");
            components.Add(component);

            component = new MarkComponent("Leader")
            {
                Title = "Linia odnośnika"
            };
            //component.AddComponent(new EntityProperty("Layer", "0"));
            //component.AddComponent(new EntityProperty("Linetype", "BYLAYER"));
            //component.AddComponent(new EntityProperty("Color", "BYLAYER"));
            AddComponent(component);

            component.Properties.Add("Layer", "0");
            component.Properties.Add("Linetype", "BYLAYER");
            component.Properties.Add("Color", "BYLAYER");
            components.Add(component);
        }

        protected override void SetComponentsEntity()
        {
            SetSignComponent();
            SetValueComponent();
            SetLeaderComponent();
        }

        protected override JigMark GetJig()
        {
            var conv = new List<IEntityConverter>
            {
                new AttributeToDBTextConverter()
            };
            return new JigVerticalConstantHorizontalMirrorMark(Entities, originPoint, basePoint, conv);
        }

        private void SetSignComponent()
        {
            var att1 = new AttributeDefinition();
            att1.SetDatabaseDefaults();
            att1.TextStyle = CADProxy.Database.Textstyle;
            att1.HorizontalMode = TextHorizontalMode.TextRight;
            att1.VerticalMode = TextVerticalMode.TextVerticalMid;
            att1.ColorIndex = 7;
            att1.Height = 2;
            att1.Position = new Point3d(-0.5, 4.5, 0);
            att1.Justify = AttachmentPoint.MiddleRight;
            att1.AlignmentPoint = new Point3d(-0.5, 4.5, 0);
            att1.Tag = "Sign";
            att1.Prompt = "Sign";
            att1.TextString = value.Sign;

            (components.Where(x => x.Name == "Sign") as MarkComponent).Entity = att1;
        }

        private void SetValueComponent()
        {
            var att2 = new AttributeDefinition();
            att2.SetDatabaseDefaults();
            att2.TextStyle = CADProxy.Database.Textstyle;
            att2.HorizontalMode = TextHorizontalMode.TextLeft;
            att2.VerticalMode = TextVerticalMode.TextVerticalMid;
            att2.ColorIndex = 7;
            att2.Height = 2;
            att2.Position = new Point3d(0.5, 4.5, 0);
            att2.Justify = AttachmentPoint.MiddleLeft;
            att2.AlignmentPoint = new Point3d(0.5, 4.5, 0);
            att2.Tag = "Value";
            att2.Prompt = "Value";
            att2.TextString = value.Value;

            (components.Where(x => x.Name == "Value") as MarkComponent).Entity = att2;
        }

        private void SetLeaderComponent()
        {
            var textArea = CADProxy.GetTextArea(CADProxy.ToDBText((components.Where(x => x.Name == "Value") as MarkComponent).Entity as AttributeDefinition));
            var pl1 = new Polyline();
            pl1.AddVertexAt(0, new Point2d(0, 5.5), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(-2, 3), 0, 0, 0);
            pl1.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X + 0.5, 3), 0, 0, 0);

            (components.Where(x => x.Name == "Leader") as MarkComponent).Entity = pl1;
        }
    }
}

