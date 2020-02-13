using CADKit.Contracts;
using CADKit.Models;
using CADKit.Proxy;
using CADKit.Runtime;
using CADKit.Utils;
using System;
using System.Collections.Generic;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;

namespace CADKit
{
    public static class Commands
    {
        [CommandMethod("CK_FLIPPALETE")]
        public static void FlipPalette()
        {

            if (AppSettings.Get.CADKitPalette != null)
            {
                AppSettings.Get.CADKitPalette.Visible = !AppSettings.Get.CADKitPalette.Visible;
            }
            else
            {
                CADProxy.Editor.WriteMessage("\nPaleta nie zainicjalizowana\n");
            }
        }

        [CommandMethod("TEST")]
        public static void TestJig()
        {
            try
            {
                var a = new ElevationMarkPNB01025();
                var group = new EntitiesSetBuilder<EntitiesSet>(a.GetEntities())
                    .AddConverter(typeof(AttributeToDBTextConverter))
                    .SetBasePoint(new Point3d(0, 0, 0))
                    .SetJig(typeof(EntittiesJig))
                    .Build()
                    .ToGroup();
            }
            catch (OperationCanceledException ex)
            {
                CADProxy.Document.Editor.WriteMessage(ex.Message);
            }
        }

        public class ElevationMarkPNB01025 : IEntitiesSetBuilder
        {
            public EntitiesSet Build()
            {
                throw new NotImplementedException();
            }

            public IEnumerable<Entity> GetEntities()
            {
                var en = new List<Entity>();

                var txt1 = new AttributeDefinition();
                txt1.SetDatabaseDefaults();
                txt1.TextStyle = CADProxy.Database.Textstyle;
                txt1.HorizontalMode = TextHorizontalMode.TextRight;
                txt1.VerticalMode = TextVerticalMode.TextVerticalMid;
                txt1.ColorIndex = 7;
                txt1.Height = 2;
                txt1.Position = new Point3d(-0.5, 4.5, 0);
                txt1.Justify = AttachmentPoint.MiddleRight;
                txt1.AlignmentPoint = new Point3d(-0.5, 4.5, 0);
                txt1.Tag = "Sign";
                txt1.Prompt = "Sign";
                txt1.TextString = "%%p";
                en.Add(txt1);

                var txt2 = new AttributeDefinition();
                txt2.SetDatabaseDefaults();
                txt2.TextStyle = CADProxy.Database.Textstyle;
                txt2.HorizontalMode = TextHorizontalMode.TextLeft;
                txt2.VerticalMode = TextVerticalMode.TextVerticalMid;
                txt2.ColorIndex = 7;
                txt2.Height = 2;
                txt2.Position = new Point3d(0.5, 4.5, 0);
                txt2.Justify = AttachmentPoint.MiddleLeft;
                txt2.AlignmentPoint = new Point3d(0.5, 4.5, 0);
                txt2.Tag = "Value";
                txt2.Prompt = "Value";
                txt2.TextString = "Some text";
                en.Add(txt2);

                var textArea = CADProxy.GetTextArea(CADProxy.ToDBText(txt2));
                var pl1 = new Polyline();
                pl1.AddVertexAt(0, new Point2d(0, 5.5), 0, 0, 0);
                pl1.AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
                pl1.AddVertexAt(0, new Point2d(-2, 3), 0, 0, 0);
                pl1.AddVertexAt(0, new Point2d(textArea[1].X - textArea[0].X + 0.5, 3), 0, 0, 0);
                en.Add(pl1);

                return en;
            }
        }
    }
}
