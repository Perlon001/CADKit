using Autofac;
using CADKit;
using CADKit.Models;
using CADKitElevationMarks.Contracts;
using System;
using System.Globalization;
using CADProxy;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.EditorInput;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
#endif

namespace CADKitElevationMarks.Models
{
    public abstract class BaseElevationMark : IElevationMark
    {
        protected readonly Matrix3d ucs = ProxyCAD.Editor.CurrentUserCoordinateSystem;
        protected readonly CoordinateSystem3d coordinateSystem = ProxyCAD.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d;
        protected readonly double scaleFactor = AppSettings.Instance.ScaleFactor;
        protected readonly IElevationMarkConfig config;
        protected readonly Matrix3d transformMatrix;
        protected readonly DrawJig jig;

        protected DBText[] texts;
        protected Point3d elevationPoint;
        protected Point3d directionPoint;

        public BaseElevationMark(IElevationMarkConfig _config, DrawJig _jig)
        {
            this.config = _config;
            this.jig = _jig;
            this.texts = new DBText[] { new DBText(), new DBText() };

            this.transformMatrix = Matrix3d.AlignCoordinateSystem(
                Point3d.Origin,
                Vector3d.XAxis,
                Vector3d.YAxis,
                Vector3d.ZAxis,
                coordinateSystem.Origin,
                coordinateSystem.Xaxis,
                coordinateSystem.Yaxis,
                coordinateSystem.Zaxis);
            
            Run();

            //using (var scope = DI.Container.BeginLifetimeScope())
            //{
            //    ITextStyleCreator bbb = new TextStyleCreator();
            //    bbb.Create(TextStyles.elevmark);

            //    ITextStyleTableService aaa = scope.Resolve<ITextStyleTableService>();
            //    var aaa = new TextStyleTableService(new TextStyleCreator());
            //    CADProxy.Database.Textstyle = scope.Resolve<ITextStyleTableService>().GetRecord(TextStyles.elevmark);
            //    CADProxy.Database.Clayer = scope.Resolve<ILayerTableService>().GetRecord(Layers.elevmark);

            //    acDoc.Database.Textstyle = scope.Resolve<IElevationMarkTextStyleGenerator>().Create<TextStyleTableRecord>();
            //}
            //TextStyleTableRecord textStyle = ((TextStyleTableRecord)transaction.GetObject(acDoc.Database.Textstyle, OpenMode.ForRead));

        }

        protected abstract void Draw(Transaction tr);

        private void Run()
        {
            GetElevationPoint();
            PrepareTextFields();
            GetDirectionPoint();
            using (var tr = ProxyCAD.Database.TransactionManager.StartTransaction())
            {
                try
                {
                    Draw(tr);
                    tr.Commit();
                }
                catch (Exception ex)
                {
                    tr.Abort();
                    throw ex;
                }
            }
//            ProxyCAD.UsingTransaction(Draw);
        }

        private void GetElevationPoint()
        {
            PromptPointResult pointResult;
            PromptPointOptions promptOptions;
            int osmode = Convert.ToInt16(Application.GetSystemVariable("Osmode"));
            int orthomode = Convert.ToInt16(Application.GetSystemVariable("Orthomode"));

            Application.SetSystemVariable("Osmode", 512);

            promptOptions = new PromptPointOptions("\nWskaż punkt wysokościowy: ");
            promptOptions.AllowNone = true;
            pointResult = ProxyCAD.Editor.GetPoint(promptOptions);
            CheckPromptStatus(osmode, orthomode, pointResult);
            
            elevationPoint = pointResult.Value;
        }

        private void GetDirectionPoint()
        {
            directionPoint = elevationPoint;
        }

        private void PrepareTextFields()
        {
            this.texts[0].TextString = GetElevationSign();
            this.texts[1].TextString = Math.Round(Math.Abs(elevationPoint.Y) * GetElevationFactor(), 3).ToString("0.000", CultureInfo.GetCultureInfo("pl-PL"));
        }

        private static void CheckPromptStatus(int osmode, int orthomode, PromptPointResult pointResult)
        {
            if (pointResult.Status == PromptStatus.Cancel || pointResult.Status == PromptStatus.None)
            {
                Application.SetSystemVariable("Osmode", osmode);
                Application.SetSystemVariable("Orthomode", orthomode);
                throw new ArgumentNullException();
            }
        }

        private string GetElevationSign()
        {
            if (Math.Round(Math.Abs(elevationPoint.Y) * GetElevationFactor(), 3) == 0)
            {
                return "%%p";
            }
            else if (elevationPoint.Y < 0)
            {
                return "-";
            }
            else
            {
                return "+";
            }
        }

        protected double GetElevationFactor()
        {
            switch (AppSettings.Instance.DrawingUnit)
            {
                case Units.m:
                    return 1;
                case Units.cm:
                    return 0.01;
                case Units.mm:
                    return 0.001;
                default:
                    throw new Exception("\nNie rozpoznana jednostka rysunkowa");
            }
        }

        //protected virtual void GetPoints()
        //{
        //    PromptPointResult pointResult;
        //    PromptPointOptions promptOptions;
        //    int osmode = Convert.ToInt16(Application.GetSystemVariable("Osmode"));
        //    int orthomode = Convert.ToInt16(Application.GetSystemVariable("Orthomode"));

        //    Application.SetSystemVariable("Osmode", 512);

        //    promptOptions = new PromptPointOptions("\nWskaż punkt wysokościowy: ");
        //    promptOptions.AllowNone = true;
        //    pointResult = ProxyCAD.Editor.GetPoint(promptOptions);
        //    CheckPromptStatus(osmode, orthomode, pointResult);
        //    point = pointResult.Value;

        //    Application.SetSystemVariable("Osmode", 0);
        //    Application.SetSystemVariable("Orthomode", 0);

        //    promptOptions.Message = "\nWskaż kierunek koty wysokościowej: ";
        //    promptOptions.UseBasePoint = true;
        //    promptOptions.BasePoint = point;
        //    pointResult = ProxyCAD.Editor.GetPoint(promptOptions);
        //    CheckPromptStatus(osmode, orthomode, pointResult);
        //    directionPoint = pointResult.Value;

        //    Application.SetSystemVariable("Osmode", osmode);
        //    Application.SetSystemVariable("Orthomode", orthomode);
        //}
    }
}
