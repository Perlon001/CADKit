#define AutoCAD

using Autofac;
using CADKit;
using CADKit.Models;
using CADKit.Services;
using CADKitElevationMarks.Contracts;
using CADProxy;
using CADProxy.Runtime;
using System;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Colors;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
// using ZwSoft.ZwCAD.GraphicsInterface;
using aaa = ZwSoft.ZwCAD;
#endif

//#if AutoCAD
//using Autodesk.AutoCAD.DatabaseServices;
//using Autodesk.AutoCAD.Geometry;
//using Autodesk.AutoCAD.ApplicationServices;
//using Autodesk.AutoCAD.DatabaseServices;
//using Autodesk.AutoCAD.GraphicsInterface;
//using bbb = Autodesk.AutoCAD;
//#endif

namespace CADKitElevationMarks
{
    public class Commands
    {
        [CommandMethod("CK_AAA")]
        public void Test()
        {
            ProxyCAD.Editor.WriteMessage("Test AAA");
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            Matrix3d ucs = ed.CurrentUserCoordinateSystem; 

            //Plane plane = new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, 1).TransformBy(ucs));

            //var points = new Point3d[] { new Point3d(0, 1000, 0), new Point3d(0, 0, 0), new Point3d(-500, 500, 0), new Point3d(1500, 500, 0) };
            //// var pline = new ZwSoft.ZwCAD.GraphicsInterface.Polyline(new Point3dCollection(points), new Vector3d(0,0,1), intPtr);
            //var pline = new Polyline();
            //foreach (var pt in points)
            //{
            //    Point3d transformedPt = pt.TransformBy(ucs);
            //    pline.AddVertexAt(pline.NumberOfVertices, plane.ParameterOf(transformedPt), 0, 0, 0);
            //}

            Color col = doc.Database.Cecolor;
            Point3dCollection pts = new Point3dCollection();
            PromptPointOptions opt = new PromptPointOptions( "\nSelect polyline vertex: " );
            opt.AllowNone = true;
            PromptPointResult res = ed.GetPoint(opt);
            while (res.Status == PromptStatus.OK)
            {
                pts.Add(res.Value);
                opt.UseBasePoint = true;
                opt.BasePoint = res.Value;
                res = ed.GetPoint(opt);
                if (res.Status == PromptStatus.OK)
                {
                    ed.DrawVector(
                      pts[pts.Count - 1], // start point
                      res.Value,          // end point
                      col.ColorIndex,     // current color
                      true);             // highlighted?
                }
            }
            //if (res.Status == PromptStatus.None)
            //{
            //    Point3d origin = new Point3d(0, 0, 0);
            //    Vector3d normal = new Vector3d(0, 0, 1);
            //    normal = normal.TransformBy(ucs);
            //    Plane plane = new Plane(origin, normal);
            //    Polyline pline = new Polyline(pts.Count);
            //    pline.Normal = normal;
            //    foreach (Point3d pt in pts)
            //    {
            //        Point3d transformedPt = pt.TransformBy(ucs);
            //        pline.AddVertexAt(pline.NumberOfVertices, plane.ParameterOf(transformedPt), 0, 0, 0);
            //    }

            //    Transaction tr = db.TransactionManager.StartTransaction();
            //    using (tr)
            //    {
            //        BlockTable bt = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
            //        BlockTableRecord btr = (BlockTableRecord)tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
            //        ObjectId plineId = btr.AppendEntity(pline);
            //        tr.AddNewlyCreatedDBObject(pline, true);
            //        tr.Commit();
            //        ed.WriteMessage("\nPolyline entity is: " + plineId.ToString());
            //    }
            //    ed.Regen();
            //}
        }

        [CommandMethod("CK_KOTA_ARCH")]
        public void ElevationMarkArch()
        {
            CreateElevationMark(ElevationMarkType.archMark);
            //object acObject = Application.ZcadApplication;
            //acObject.GetType().InvokeMember("ZoomExtents", BindingFlags.InvokeMethod, null, acObject, null);
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

            try
            {
                var elevationMarkFactory = GetElevationMarkFactory(AppSettings.Instance.DrawingStandard);
                elevationMarkFactory.Create(type);
            }
            catch (System.Exception ex)
            {
                ProxyCAD.Editor.WriteMessage(ex.Message);
            }
            finally
            {
                SystemVariableService.RestoreSystemVariables(variables);
            }
        }

        private IElevationMarkFactory GetElevationMarkFactory(DrawingStandards standards)
        {
            IElevationMarkFactory factory;
            using (ILifetimeScope scope = DI.Container.BeginLifetimeScope())
            {
                switch (standards)
                {
                    case DrawingStandards.PN_B_01025:
                        factory = scope.Resolve<IElevationMarkFactoryPNB01025>();
                        break;
                    case DrawingStandards.CADKIT:
                        factory = scope.Resolve<IElevationMarkFactoryCADKit>();
                        break;
                    default:
                        throw new NotImplementedException($"Brak implemetacji standardu {standards.ToString()}");
                }
            }

            return factory;
        }
    }
}
