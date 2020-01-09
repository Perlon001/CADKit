using CADProxy;
using CADProxy.Runtime;
using CADKit.Extensions;
using CADKit.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using CADProxy.Extensions;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.GraphicsInterface;

using Polyline = ZwSoft.ZwCAD.DatabaseServices.Polyline;

#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.GraphicsInterface;

using Polyline = Autodesk.AutoCAD.DatabaseServices.Polyline;

#endif


namespace CADKit
{
    public enum markPosition { left, right };
    public class JigTest
    {

        IEnumerable<Entity> markEntities;
        MarkDrawJig jig;
        

        public JigTest()
        {
        }

        public void Draw()
        {

            PromptPointOptions promptPointOptions = new PromptPointOptions("Select reference point:");
            PromptPointResult promptPoint = ProxyCAD.Editor.GetPoint(promptPointOptions);

            if (promptPoint.Status != PromptStatus.OK)
                return;

            markEntities = GetEntities(markPosition.right);
            markEntities.TransformBy(Matrix3d.Displacement(new Point3d(0, 0, 0).GetVectorTo(promptPoint.Value)));
            Group group = markEntities.ToGroup();

            using (var tr = ProxyCAD.Document.TransactionManager.StartTransaction())
            {
                jig = new HorizontalMoveGeometryJig(
                    group
                    .GetAllEntityIds()
                    .Select(ent => (Entity)ent
                    .GetObject(OpenMode.ForWrite)
                    .Clone())
                    .ToList(), promptPoint.Value);

                (group.ObjectId.GetObject(OpenMode.ForWrite) as Group).SetVisibility(false);

                PromptResult result = ProxyCAD.Editor.Drag(jig);
                if (result.Status == PromptStatus.OK)
                {
                    foreach (var p in markEntities)
                    {
                        p.TransformBy(jig.Transforms);
                    }
                    (group.ObjectId.GetObject(OpenMode.ForWrite) as Group).SetVisibility(true);
                    tr.Commit();
                }
                else
                {
                    tr.Abort();
                }
            }
        }

        private IEnumerable<Entity> GetEntities(markPosition position)
        {
            DBText[] texts = new DBText[] { new DBText(), new DBText() };
            Polyline[] plines = new Polyline[] { new Polyline(), new Polyline() };

            
            foreach (var item in texts)
            {
                item.SetDatabaseDefaults();
                item.TextStyle = ProxyCAD.Database.Textstyle;
                item.VerticalMode = TextVerticalMode.TextVerticalMid;
                item.ColorIndex = 7;
                item.Height = 2;
            }
            texts[0].HorizontalMode = TextHorizontalMode.TextRight;
            texts[1].HorizontalMode = TextHorizontalMode.TextLeft;

            texts[0].AlignmentPoint = new Point3d(1, 4.5, 0);
            texts[0].TextString = "%%p";

            texts[1].AlignmentPoint = new Point3d(1.5, 4.5, 0);
            texts[1].TextString = "0.000";
            var textArea = ProxyCAD.GetTextArea(texts[1]);

            plines[0].AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            plines[0].AddVertexAt(0, new Point2d(0, 3), 0, 0, 0);
            double posX = textArea[1].X - textArea[0].X + 1.5;
            switch (position)  
            {
                case markPosition.left:
                    posX = -(textArea[1].X - textArea[0].X + 1.5);
                    break;
            }
            
            plines[0].AddVertexAt(0, new Point2d(posX, 3), 0, 0, 0);

            plines[1].AddVertexAt(0, new Point2d(-1, 1), 0, 0, 0);
            plines[1].AddVertexAt(0, new Point2d(0, 0), 0, 0, 0);
            plines[1].AddVertexAt(0, new Point2d(1, 1), 0, 0, 0);

            List<Entity> en = new List<Entity>();
            foreach (var item in texts)
            {
                en.Add(item);
            }
            foreach (var item in plines)
            {
                en.Add(item);
            }

            return en;
        }
    }

    public abstract class MarkDrawJig : DrawJig
    {
        protected Point3d currentPoint;
        protected Point3d basePoint;
        protected IEnumerable<Entity> entityList;
        public Matrix3d Transforms { get; protected set; }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            JigPromptPointOptions jigOpt = new JigPromptPointOptions("Select insertion point:");
            jigOpt.UserInputControls = UserInputControls.Accept3dCoordinates;
            jigOpt.BasePoint = basePoint;

            PromptPointResult res = prompts.AcquirePoint(jigOpt);

            if (res.Value.IsEqualTo(basePoint))
            {
                return SamplerStatus.NoChange;
            }
            currentPoint = res.Value;

            return SamplerStatus.OK;
        }

        protected virtual void ListEntityUpdate(IEnumerable<Entity> _listEntity)
        {

        }
    }

    public class SimpleGeometryJig : MarkDrawJig
    {
        public SimpleGeometryJig(IEnumerable<Entity> _entityList, Point3d _basePoint) : base()
        {
            entityList = _entityList;
            currentPoint = _basePoint;
            basePoint = _basePoint;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            try
            {
                Transforms = Matrix3d.Displacement(basePoint.GetVectorTo(currentPoint));
                var geometry = draw.Geometry;
                if (geometry != null)
                {
                    geometry.PushModelTransform(Transforms);
                    foreach (var entity in entityList)
                    {
                        geometry.Draw(entity);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ProxyCAD.Editor.WriteMessage(ex.Message);
                return false;
            }
        }
    }

    public class HorizontalMoveGeometryJig : MarkDrawJig
    {
        private bool leftDraw = false;
        public HorizontalMoveGeometryJig(IEnumerable<Entity> _entityList, Point3d _basePoint) : base()
        {
            entityList = _entityList;
            currentPoint = _basePoint;
            basePoint = _basePoint;
        }

        protected override void ListEntityUpdate(IEnumerable<Entity> _entityList)
        {
            entityList = _entityList;
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            var result = base.Sampler(prompts);
            if (result != SamplerStatus.OK)
            {
                return result;
            }
            if (leftDraw)
            {
                foreach(var e in entityList.Where(ent => ent.GetType() == typeof(Polyline))) 
                {
                    e.TransformBy(Matrix3d.Mirroring(new Line3d(new Point3d(0,0,0),new Vector3d(0,1,0))));
                }
                ListEntityUpdate(entityList);
            }

            return SamplerStatus.OK;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            try
            {
                Transforms = Matrix3d.Displacement(basePoint.GetVectorTo(new Point3d(currentPoint.X, basePoint.Y, currentPoint.Z)));
                WorldGeometry geometry = draw.Geometry;
                if (geometry != null)
                {
                    geometry.PushModelTransform(Transforms);
                    foreach (var entity in entityList)
                    {
                        geometry.Draw(entity);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ProxyCAD.Editor.WriteMessage(ex.Message);
                return false;
            }
        }
    }

    public class JIGCommand
    {
        [CommandMethod("CK_AAA")]
        public static void TestAAA()
        {
            var jig = new JigTest();
            jig.Draw();
        }
    }
}
