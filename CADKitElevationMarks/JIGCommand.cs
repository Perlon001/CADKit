using CADProxy.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.GraphicsInterface;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.GraphicsInterface;
#endif


namespace CADKitElevationMarks
{
    public class JIGCommand
    {
        [CommandMethod("CK_AAA")]
        public static void TestAAA()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            PromptSelectionOptions promptSelection = new PromptSelectionOptions();
            PromptSelectionResult result = ed.GetSelection(promptSelection);
            if (result.Status != PromptStatus.OK)
                return;
            using (Transaction t = doc.TransactionManager.StartTransaction())
            {
                var clones = result.Value
                    .Cast<SelectedObject>()
                    .Select(so => (Entity)t.GetObject(so.ObjectId, OpenMode.ForRead))
                    .Select(ent => (Entity)ent.Clone())
                    .ToList();


                PromptPointOptions promptPoint = new PromptPointOptions("Select reference point:");
                PromptPointResult promptPointResult = ed.GetPoint(promptPoint);
                if (promptPointResult.Status != PromptStatus.OK)
                    return;

                List<Entity> entities = new List<Entity>();
                foreach (ObjectId oid in result.Value.GetObjectIds())
                {
                    DBObject ent = t.GetObject(oid, OpenMode.ForWrite);
                    Entity p = ent as Entity;
                    if (p == null)
                        continue;
//                    p.Visible = false;
                    entities.Add(p);
                }
                t.Commit();

                SimpleGeometryJig jig = new SimpleGeometryJig(clones, promptPointResult.Value.TransformBy((ed.CurrentUserCoordinateSystem)), ed);
                PromptResult res = ed.Drag(jig);
                if(res.Status == PromptStatus.OK)
                {
                    ed.WriteMessage("Commit na obiektach");
                    var space = db.CurrentSpaceId;
                    var tab = t.GetObject(space, OpenMode.ForWrite);
                    var cSpace = (BlockTableRecord)tab;
                    foreach (var p in entities)
                    {
                        p.TransformBy(jig.Transforms);
                        cSpace.AppendEntity(p);
                        t.AddNewlyCreatedDBObject(p, true);
                        p.Visible = true;
                    }
                }
                //foreach (var p in clones)
                //{
                //    p.Dispose();
                //}
                t.Commit();
            }
            //object acObject = Application.ZcadApplication;
            //acObject.GetType().InvokeMember("ZoomExtents", BindingFlags.InvokeMethod, null, acObject, null);

        }

        public class SimpleGeometryJig : DrawJig
        {

            private readonly IEnumerable<Entity> polylines;
            private Point3d currentPosition;
            private Point3d referencePosition;
            private Editor ed;
            public Matrix3d Transforms { get; private set; }

            public SimpleGeometryJig(IEnumerable<Entity> _polylines, Point3d _referencePoint, Editor _ed)
            {
                polylines = _polylines;
                currentPosition = _referencePoint;
                referencePosition = _referencePoint;
                ed = _ed;
                foreach(var p in polylines)
                {
                    // p.Visible = false;
                }
            }

            protected override SamplerStatus Sampler(JigPrompts prompts)
            {
                JigPromptPointOptions jigOpt = new JigPromptPointOptions("Select insertion point:");
                jigOpt.UserInputControls = UserInputControls.Accept3dCoordinates;
                jigOpt.BasePoint = referencePosition;

                PromptPointResult res = prompts.AcquirePoint(jigOpt);

                if (res.Value.IsEqualTo(currentPosition))
                {
                    return SamplerStatus.NoChange;
                }
                currentPosition = res.Value;

                return SamplerStatus.OK;
            }

            protected override bool WorldDraw(WorldDraw draw)
            {
                try
                {
                    var currentLocation = new Point3d(currentPosition.X, referencePosition.Y, currentPosition.Z);
                    Transforms = Matrix3d.Displacement(referencePosition.GetVectorTo(currentLocation));
                    var geometry = draw.Geometry;
                    if (geometry != null)
                    {
                        if (currentPosition.X < referencePosition.X)
                        {
                            geometry.PushModelTransform(Matrix3d.Mirroring(new Line3d(referencePosition, new Vector3d(0, 1, 0))));
                        }
                        if (currentPosition.Y < referencePosition.Y)
                        {
                            geometry.PushModelTransform(Matrix3d.Mirroring(new Line3d(referencePosition, new Vector3d(1, 0, 0))));
                        }
                        geometry.PushModelTransform(Matrix3d.Displacement(referencePosition.GetVectorTo(currentLocation)));
                        foreach (var entity in polylines)
                        {
                            geometry.Draw(entity);
                        }
                        geometry.PopModelTransform();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    ed.WriteMessage(ex.Message);
                    return false;
                }
            }
        }
    }
}
