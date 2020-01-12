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
                    value = new ElevationValue("",textValue.StringResult).Parse();
                    using (ProxyCAD.Document.LockDocument())
                    {
                        CreateEntityList();
                        entityList.TransformBy(Matrix3d.Scaling(AppSettings.Instance.ScaleFactor, new Point3d(0, 0, 0)));
                        var group = entityList.ToGroup();
                        using (var tr = ProxyCAD.Document.TransactionManager.StartTransaction())
                        {
                            var jig = GetMarkJig(group, new Point3d(0, 0, 0));
                            (group.ObjectId.GetObject(OpenMode.ForWrite) as Group).SetVisibility(false);
                            var result = ProxyCAD.Editor.Drag(jig);
                            GroupErase(tr, group);
                            if (result.Status == PromptStatus.OK)
                            {
                                group = jig.GetEntity().ToList().ToGroup();
                            }
                            Utils.FlushGraphics();
                            tr.Commit();
                        }
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

        protected override void CreateEntityList()
        {
            var en = new List<Entity>();

            var tx1 = new DBText();
            tx1.SetDatabaseDefaults();
            tx1.TextStyle = ProxyCAD.Database.Textstyle;
            tx1.HorizontalMode = TextHorizontalMode.TextLeft;
            tx1.VerticalMode = TextVerticalMode.TextVerticalMid;
            tx1.ColorIndex = 7;
            tx1.Height = 2;
            tx1.AlignmentPoint = new Point3d(2, 1.5, 0);
            tx1.TextString = this.value.Sign + this.value.Value;
            en.Add(tx1);

            var l1 = new Line(new Point3d(-1.5, -1.5, 0), new Point3d(1.5, 1.5, 0));
            en.Add(l1);

            var l2 = new Line(new Point3d(-1.5, 1.5, 0), new Point3d(1.5, -1.5, 0));
            en.Add(l2);

            var textArea = ProxyCAD.GetTextArea(tx1);
            var l3 = new Line(new Point3d(0, 0, 0), new Point3d(textArea[1].X - textArea[0].X + 2, 0, 0));
            en.Add(l3);

            this.entityList = en;
        }

        protected override EntityListJig GetMarkJig(Group _group, Point3d _point)
        {
            return new JigVerticalMirrorMark(
                _group.GetAllEntityIds()
                .Select(ent => (Entity)ent
                .GetObject(OpenMode.ForWrite)
                .Clone())
                .ToList(),
                _point);
        }

        protected override EntityListJig GetMarkJig(IEnumerable<Entity> listEntity, Point3d point)
        {
            throw new NotImplementedException();
        }

        protected override void InsertMarkBlock(Point3d insertPoint)
        {
            throw new NotImplementedException();
        }
    }
}
