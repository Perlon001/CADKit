using System;
using System.Linq;

using Autofac;

using CADKit;
using CADKit.Extensions;
using CADKit.Models;
using CADKit.Services;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Models;
using CADProxy;
using CADProxy.Runtime;

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

namespace CADKitElevationMarks
{
    public class ElevationMarkCommands
    {
        [CommandMethod("CK_KOTA_ARCH")]
        public void ElevationMarkArch()
        {
            CreateElevationMark(ElevationMarkType.archMark);
        }

        [CommandMethod("CK_KOTA_KONSTR")]
        public void ElevationMarkConstr()
        {
            CreateElevationMark(ElevationMarkType.constrMark);
        }

        [CommandMethod("CK_KOTA_POZIOM")]
        public void ElevationMarkPlate()
        {
            CreateElevationMark(ElevationMarkType.planeMark);
        }

        private void CreateElevationMark(ElevationMarkType type)
        {
            SystemVariables variables = SystemVariableService.GetSystemVariables();
            PromptPointOptions promptPointOptions = new PromptPointOptions("Wskaż punkt wysokościowy:");
            PromptPointResult basePoint = ProxyCAD.Editor.GetPoint(promptPointOptions);
            if (basePoint.Status != PromptStatus.OK) return;

            var elevationMarkFactory = CreateElevationMarkFactory(AppSettings.Instance.DrawingStandard);
            var elevationMark = elevationMarkFactory.CreateElevationMarkFactory(type, new ElevationValue("%%p", basePoint.Value.Y));
            Group group = elevationMark.EntityList
                .TransformBy(Matrix3d.Displacement(new Point3d(0, 0, 0).GetVectorTo(basePoint.Value)))
                .ToList()
                .ToGroup();
            using (var tr = ProxyCAD.Document.TransactionManager.StartTransaction())
            {
                var jig = new JigDisplacement(
                    group.GetAllEntityIds()
                    .Select(ent => (Entity)ent
                    .GetObject(OpenMode.ForWrite)
                    .Clone())
                    .ToList(),
                    basePoint.Value);
                (group.ObjectId.GetObject(OpenMode.ForWrite) as Group).SetVisibility(false);
                PromptResult result = ProxyCAD.Editor.Drag(jig);
                if (result.Status == PromptStatus.OK)
                {
                    foreach (var p in elevationMark.EntityList)
                    {
                        p.TransformBy(jig.Transforms);
                    }
                    (group.ObjectId.GetObject(OpenMode.ForWrite) as Group).SetVisibility(true);
                }
                else
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
                tr.Commit();
            }
            SystemVariableService.RestoreSystemVariables(variables);
        }

        private ElevationMarkFactory CreateElevationMarkFactory(DrawingStandards standards)
        {
            ElevationMarkFactory factory;
            using (ILifetimeScope scope = DI.Container.BeginLifetimeScope())
            {
                switch (standards)
                {
                    case DrawingStandards.PN_B_01025:
                        //factory = scope.Resolve<IElevationMarkFactoryPNB01025>();
                        factory = new ElevationMarkFactoryPNB01025();
                        break;
                    case DrawingStandards.CADKIT:
                        //factory = scope.Resolve<IElevationMarkFactoryCADKit>();
                        factory = new ElevationMarkFactoryCADKit();
                        break;
                    default:
                        throw new NotImplementedException($"Brak implemetacji standardu {standards.ToString()}");
                }
            }

            return factory;
        }

        //[CommandMethod("TEST01")]
        //public static void Test01()
        //{
        //    var doc = Application.DocumentManager.MdiActiveDocument;
        //    var db = doc.Database;
        //    var ed = doc.Editor;
        //    Matrix3d ucs = ed.CurrentUserCoordinateSystem;
        //    Plane plane = new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, 1).TransformBy(ucs));

        //    // Generate pline
        //    var points = new Point3d[] { new Point3d(0, 1000, 0), new Point3d(0, 0, 0), new Point3d(-500, 500, 0), new Point3d(1500, 500, 0) };
        //    var pline = new ZwSoft.ZwCAD.DatabaseServices.Polyline();
        //    foreach (var pt in points)
        //    {
        //        Point3d transformedPt = pt.TransformBy(ucs);
        //        pline.AddVertexAt(pline.NumberOfVertices, plane.ParameterOf(transformedPt), 0, 0, 0);
        //    }

        //    var psr = ed.GetSelection();
        //    if (psr.Status != PromptStatus.OK) return;
        //    var ppr = ed.GetPoint("\nSpecify first point of mirror line: ");
        //    if (ppr.Status != PromptStatus.OK) return;

        //    using (var tr = db.TransactionManager.StartTransaction())
        //    {

        //        ObjectIdCollection ids = new ObjectIdCollection();
        //        using (var tr1 = db.TransactionManager.StartTransaction())
        //        {
        //            BlockTable bt = (BlockTable)tr1.GetObject(db.BlockTableId, OpenMode.ForRead);
        //            BlockTableRecord btr = (BlockTableRecord)tr1.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
        //            ObjectId plineId = btr.AppendEntity(pline);
        //            tr1.AddNewlyCreatedDBObject(pline, true);
        //            tr1.Commit();
        //            object acObject = Application.ZcadApplication;
        //            acObject.GetType().InvokeMember("ZoomExtents", BindingFlags.InvokeMethod, null, acObject, null);

        //            ed.WriteMessage("\nPolyline entity is: " + plineId.ToString());
        //            ids.Add(plineId);
        //        }

        //        IEnumerable<Entity> clones;
        //        //clones = ids.Cast<SelectedObject>().Select(so => (Entity)tr.GetObject(so.ObjectId, OpenMode.ForWrite));
        //        clones = psr.Value
        //           .Cast<SelectedObject>()
        //           .Select(so => (Entity)tr.GetObject(so.ObjectId, OpenMode.ForWrite));
        //        //    //.Select(ent => (Entity)ent.Clone());
        //        //    //.ToArray();
        //        var jig = new PlacementJig(clones, ppr.Value.TransformBy(ed.CurrentUserCoordinateSystem));
        //        var pr = ed.Drag(jig);

        //        if (pr.Status == PromptStatus.OK)
        //        {
        //            var cSpace = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);
        //            foreach (var entity in clones)
        //            {
        //                entity.TransformBy(jig.Displacement);
        //                // cSpace.AppendEntity(entity);
        //                // tr.AddNewlyCreatedDBObject(entity, true);
        //            }
        //        }
        //        else
        //        {
        //            foreach (var entity in clones)
        //            {
        //                entity.Dispose();
        //            }
        //        }
        //        tr.Commit();
        //    }
        //}

        //[CommandMethod("MYMOVE", ZwSoft.ZwCAD.Runtime.CommandFlags.UsePickSet)]

        //public static void CustomMoveCmd()

        //{
        //    Document doc = Application.DocumentManager.MdiActiveDocument;
        //    Editor ed = doc.Editor;
        //    PromptSelectionResult psr = ed.GetSelection();
        //    if (psr.Status != PromptStatus.OK || psr.Value.Count == 0) return;
        //    ObjectIdCollection ids = new ObjectIdCollection(psr.Value.GetObjectIds());
        //    PromptPointResult ppr = ed.GetPoint("\nSpecify base point: ");
        //    if (ppr.Status != PromptStatus.OK) return;
        //    Point3d basePt = ppr.Value;
        //    Point3d curPt = basePt;
        //    // A local delegate for our event handler so
        //    // we can remove it at the end
        //    PointMonitorEventHandler handler = null;
        //    // Our transaction
        //    Transaction tr = doc.Database.TransactionManager.StartTransaction();
        //    using (tr)
        //    {
        //        // Create our transient drawables, with associated
        //        // graphics, from the selected objects
        //        List<Drawable> drawables = CreateTransGraphics(tr, ids);
        //        try
        //        {
        //            // Add our point monitor
        //            // (as a delegate we have access to basePt and curPt,
        //            //  which avoids having to access global/member state)
        //            handler = delegate (object sender, PointMonitorEventArgs e)
        //            {
        //                // Get the point, with "ortho" applied, if needed
        //                Point3d pt = e.Context.RawPoint;
        //                if (IsOrthModeOn()) pt = GetOrthoPoint(basePt, pt);
        //                // Update our graphics and the current point
        //                UpdateTransGraphics(drawables, curPt, pt);
        //                curPt = pt;
        //            };
        //            ed.PointMonitor += handler;
        //            // Ask for the destination, during which the point
        //            // monitor will be updating the transient graphics
        //            PromptPointOptions opt = new PromptPointOptions("\nSpecify second point: ");
        //            opt.UseBasePoint = true;
        //            opt.BasePoint = basePt;
        //            ppr = ed.GetPoint(opt);
        //            // If the point was selected successfully...
        //            if (ppr.Status == PromptStatus.OK)
        //            {
        //                // ... move the entities to their destination
        //                MoveEntities( tr, basePt, IsOrthModeOn() ? GetOrthoPoint(basePt, ppr.Value) : ppr.Value, ids );
        //                // And inform the user
        //                ed.WriteMessage( "\n{0} object{1} moved", ids.Count, ids.Count == 1 ? "" : "s" );
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ed.WriteMessage("\nException: {0}", ex.Message);
        //        }
        //        finally
        //        {
        //            // Clear any transient graphics
        //            ClearTransGraphics(drawables);
        //            // Remove the event handler
        //            if (handler != null)
        //                ed.PointMonitor -= handler;
        //            tr.Commit();
        //            tr.Dispose();
        //        }
        //    }
        //}
        //private static bool IsOrthModeOn()
        //{
        //    // Check the value of the ORTHOMODE sysvar
        //    object orth = Application.GetSystemVariable("ORTHOMODE");
        //    return Convert.ToInt32(orth) > 0;
        //}
        //private static Point3d GetOrthoPoint( Point3d basePt, Point3d pt )
        //{
        //    // Apply a crude orthographic mode
        //    double x = pt.X;
        //    double y = pt.Y;
        //    Vector3d vec = basePt.GetVectorTo(pt);
        //    if (Math.Abs(vec.X) >= Math.Abs(vec.Y))
        //        y = basePt.Y;
        //    else
        //        x = basePt.X;

        //    return new Point3d(x, y, 0.0);
        //}        
        //private static void MoveEntities( Transaction tr, Point3d basePt, Point3d moveTo, ObjectIdCollection ids )
        //{
        //    // Transform a set of entities to a new location
        //    Matrix3d mat = Matrix3d.Displacement(basePt.GetVectorTo(moveTo));
        //    foreach (ObjectId id in ids)
        //    {
        //        Entity ent = (Entity)tr.GetObject(id, OpenMode.ForWrite);
        //        ent.TransformBy(mat);
        //    }
        //}
        //private static List<Drawable> CreateTransGraphics( Transaction tr, ObjectIdCollection ids )
        //{
        //    // Create our list of drawables to return
        //    List<Drawable> drawables = new List<Drawable>();
        //    foreach (ObjectId id in ids)
        //    {
        //        // Read each entity
        //        Entity ent = (Entity)tr.GetObject(id, OpenMode.ForRead);
        //        // Clone it, make it red & add the clone to the list
        //        Entity drawable = ent.Clone() as Entity;
        //        drawable.ColorIndex = 1;
        //        drawables.Add(drawable);
        //    }

        //    // Draw each one initially
        //    foreach (Drawable d in drawables)
        //    {
        //        TransientManager.CurrentTransientManager.AddTransient( d, TransientDrawingMode.DirectShortTerm, 128, new IntegerCollection() );
        //    }

        //    return drawables;
        //}
        //private static void UpdateTransGraphics( List<Drawable> drawables, Point3d curPt, Point3d moveToPt )
        //{
        //    // Displace each of our drawables
        //    Matrix3d mat = Matrix3d.Displacement(curPt.GetVectorTo(moveToPt));
        //    // Update their graphics
        //    foreach (Drawable d in drawables)
        //    {
        //        Entity e = d as Entity;
        //        e.TransformBy(mat);
        //        TransientManager.CurrentTransientManager.UpdateTransient( d, new IntegerCollection() );
        //    }
        //}
        //private static void ClearTransGraphics( List<Drawable> drawables )
        //{
        //    // Clear the transient graphics for our drawables
        //    TransientManager.CurrentTransientManager.EraseTransients( TransientDrawingMode.DirectShortTerm, 128, new IntegerCollection() );
        //    // Dispose of them and clear the list
        //    foreach (Drawable d in drawables)
        //    {
        //        d.Dispose();
        //    }
        //    drawables.Clear();
        //}


        //[CommandMethod("CK_BBB")]
        //public void TestBBB()
        //{
        //    ProxyCAD.Editor.WriteMessage("Test BBB");
        //    Document doc = Application.DocumentManager.MdiActiveDocument;
        //    Database db = doc.Database;
        //    Editor ed = doc.Editor;
        //    Matrix3d ucs = ed.CurrentUserCoordinateSystem; 
        //    Plane plane = new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, 1).TransformBy(ucs));

        //    // Generate pline (group of entities) mayby from external class ???
        //    var points = new Point3d[] { new Point3d(0, 1000, 0), new Point3d(0, 0, 0), new Point3d(-500, 500, 0), new Point3d(1500, 500, 0) };
        //    var pline = new ZwSoft.ZwCAD.DatabaseServices.Polyline();
        //    foreach (var pt in points)
        //    {
        //        Point3d transformedPt = pt.TransformBy(ucs);
        //        pline.AddVertexAt(pline.NumberOfVertices, plane.ParameterOf(transformedPt), 0, 0, 0);
        //    }

        //    #region a
        //    //Color col = doc.Database.Cecolor;
        //    //Point3dCollection pts = new Point3dCollection();
        //    //PromptPointOptions opt = new PromptPointOptions( "\nSelect polyline vertex: " );
        //    //opt.AllowNone = true;
        //    //PromptPointResult res = ed.GetPoint(opt);
        //    //while (res.Status == PromptStatus.OK)
        //    //{
        //    //    pts.Add(res.Value);
        //    //    opt.UseBasePoint = true;
        //    //    opt.BasePoint = res.Value;
        //    //    res = ed.GetPoint(opt);
        //    //    if (res.Status == PromptStatus.OK)
        //    //    {
        //    //        ed.DrawVector(
        //    //          pts[pts.Count - 1], // start point
        //    //          res.Value,          // end point
        //    //          col.ColorIndex,     // current color
        //    //          true);             // highlighted?
        //    //    }
        //    //}

        //    //if (res.Status == PromptStatus.None)
        //    //{
        //    //    Point3d origin = new Point3d(0, 0, 0);
        //    //    Vector3d normal = new Vector3d(0, 0, 1);
        //    //    normal = normal.TransformBy(ucs);
        //    //    Plane plane = new Plane(origin, normal);
        //    //    Polyline pline = new Polyline(pts.Count);
        //    //    pline.Normal = normal;
        //    //    foreach (Point3d pt in pts)
        //    //    {
        //    //        Point3d transformedPt = pt.TransformBy(ucs);
        //    //        pline.AddVertexAt(pline.NumberOfVertices, plane.ParameterOf(transformedPt), 0, 0, 0);
        //    //    }
        //    #endregion
        //    // End generate group
        //    PointMonitorEventHandler handler = null;
        //    Transaction tr = db.TransactionManager.StartTransaction();
        //    using (tr)
        //    {
        //        BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
        //        // BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
        //        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

        //        var normal = Vector3d.ZAxis.TransformBy(ed.CurrentUserCoordinateSystem);
        //        plane = new Plane(Point3d.Origin,normal);

        //        pline.Normal = normal;
        //        ObjectId plineId = btr.AppendEntity(pline);
        //        tr.AddNewlyCreatedDBObject(pline, true);

        //        object acObject = Application.ZcadApplication;
        //        acObject.GetType().InvokeMember("ZoomExtents", BindingFlags.InvokeMethod, null, acObject, null);

        //        ed.WriteMessage("\nPolyline entity is: " + plineId.ToString());
                
        //        ObjectIdCollection ids = new ObjectIdCollection(new ObjectId[] { plineId });
        //        List<Drawable> drawables = CreateTransGraphics(tr, ids);
        //        foreach (ObjectId ent in ids)
        //        {
        //            var e = tr.GetObject(ent, OpenMode.ForWrite) as Entity;
        //            e.Visible = false;
        //        }
        //        // (psr.Value.GetObjectIds());
        //        // or ids.Add(plineId);

        //        try
        //        {
        //            // set current point as base point
        //            Point3d basePt = new Point3d(0, 0, 0);
        //            Point3d curPt = basePt;
                    
        //            handler = delegate (object sender, PointMonitorEventArgs e)
        //            {
        //                // Get the point, with "ortho" applied, if needed
        //                Point3d pt = e.Context.RawPoint;
        //                //if (IsOrthModeOn()) pt = GetOrthoPoint(basePt, pt);
        //                // Update our graphics and the current point
        //                UpdateTransGraphics(drawables, curPt, pt);
        //                curPt = pt;
        //            };
        //            ed.PointMonitor += handler;

        //            // Ask for the destination, during which the point
        //            // monitor will be updating the transient graphics
        //            PromptPointOptions opt = new PromptPointOptions("\nSpecify second point: ");
        //            opt.UseBasePoint = true;
        //            opt.BasePoint = curPt;
                    
        //            var ppr = ed.GetPoint(opt);
                    
        //            // If the point was selected successfully...
        //            if (ppr.Status == PromptStatus.OK)
        //            {
        //                // ... move the entities to their destination
        //                // MoveEntities(tr, basePt, IsOrthModeOn() ? GetOrthoPoint(basePt, ppr.Value) : ppr.Value, ids);
        //                MoveEntities(tr, basePt, ppr.Value, ids);
        //                foreach(ObjectId ent in ids)
        //                {
        //                    var e = tr.GetObject(ent,OpenMode.ForWrite) as Entity;
        //                    e.Visible = true;
        //                }
        //                // And inform the user
        //                ed.WriteMessage("\n{0} object{1} moved", ids.Count, ids.Count == 1 ? "" : "s");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ed.WriteMessage("\nException: {0}", ex.Message);
        //        }
        //        finally
        //        {
        //            // Clear any transient graphics
        //            ClearTransGraphics(drawables);
        //            // Remove the event handler
        //            if (handler != null) ed.PointMonitor -= handler;
        //            tr.Commit();
        //            tr.Dispose();
        //        }
        //    }
        //    // ed.Regen();
        //}

        //[CommandMethod("CK_CCC")]
        //public void TestCCC()
        //{
        //    ProxyCAD.Editor.WriteMessage("Test AAA");
        //    Document doc = Application.DocumentManager.MdiActiveDocument;
        //    Database db = doc.Database;
        //    Editor ed = doc.Editor;
        //    Matrix3d ucs = ed.CurrentUserCoordinateSystem;
        //    Plane plane = new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, 1).TransformBy(ucs));

        //    // Generate pline (group of entities) mayby from external class ???
        //    var points = new Point3d[] { new Point3d(0, 1000, 0), new Point3d(0, 0, 0), new Point3d(-500, 500, 0), new Point3d(1500, 500, 0) };
        //    var pline = new ZwSoft.ZwCAD.DatabaseServices.Polyline();
        //    foreach (var pt in points)
        //    {
        //        Point3d transformedPt = pt.TransformBy(ucs);
        //        pline.AddVertexAt(pline.NumberOfVertices, plane.ParameterOf(transformedPt), 0, 0, 0);
        //    }

        //    // End generate group

        //    PointMonitorEventHandler handler = null;
        //    Transaction tr = db.TransactionManager.StartTransaction();
        //    using (tr)
        //    {
        //        BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
        //        // BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
        //        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(db.CurrentSpaceId, OpenMode.ForWrite);

        //        var normal = Vector3d.ZAxis.TransformBy(ed.CurrentUserCoordinateSystem);
        //        plane = new Plane(Point3d.Origin, normal);

        //        pline.Normal = normal;
        //        ObjectId plineId = btr.AppendEntity(pline);
        //        tr.AddNewlyCreatedDBObject(pline, true);

        //        object acObject = Application.ZcadApplication;
        //        acObject.GetType().InvokeMember("ZoomExtents", BindingFlags.InvokeMethod, null, acObject, null);

        //        ed.WriteMessage("\nPolyline entity is: " + plineId.ToString());
        //        ObjectIdCollection ids = new ObjectIdCollection(new ObjectId[] { plineId });

        //        var jig = new KotaJig(ids, new Point3d(0, 0, 0));
        //        while(true)
        //        {
        //            PromptResult res = ed.Drag(jig);
        //            switch (res.Status)
        //            {
        //                case PromptStatus.OK:
        //                    {
        //                        break;
        //                    }
        //                case PromptStatus.None:
        //                    {
        //                        tr.Commit();
        //                        return;
        //                    }
        //                default:
        //                    return;
        //            }
        //        }
        //    }
        //}
    }
}
