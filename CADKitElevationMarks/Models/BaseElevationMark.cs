using Autofac;
using CADKit;
using CADKit.Models;
using CADKitElevationMarks.Contracts;
using System;
using System.Globalization;
using CADProxy;
using CADKit.Utils;

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
        protected readonly IElevationMarkConfig config;
        protected readonly Matrix3d transformMatrix;
        protected readonly Matrix3d ucs;
        protected readonly CoordinateSystem3d coordinateSystem;
        protected readonly double scaleFactor;


        protected DBText[] texts;
        protected Point3d point;
        protected Point3d placePoint;

        public BaseElevationMark(IElevationMarkConfig _config)
        {
            this.config = _config;
            this.scaleFactor = AppSettings.Instance.ScaleFactor;
            this.texts = new DBText[2];
            this.ucs = ProxyCAD.Editor.CurrentUserCoordinateSystem;
            this.coordinateSystem = ProxyCAD.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d;
            this.transformMatrix = Matrix3d.AlignCoordinateSystem(
                Point3d.Origin,
                Vector3d.XAxis,
                Vector3d.YAxis,
                Vector3d.ZAxis,
                coordinateSystem.Origin,
                coordinateSystem.Xaxis,
                coordinateSystem.Yaxis,
                coordinateSystem.Zaxis);
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                //ITextStyleCreator bbb = new TextStyleCreator();
                //bbb.Create(TextStyles.elevmark);

                //ITextStyleTableService aaa = scope.Resolve<ITextStyleTableService>();
                // var aaa = new TextStyleTableService(new TextStyleCreator());
                //CADProxy.Database.Textstyle = scope.Resolve<ITextStyleTableService>().GetRecord(TextStyles.elevmark);
                //CADProxy.Database.Clayer = scope.Resolve<ILayerTableService>().GetRecord(Layers.elevmark);

                //acDoc.Database.Textstyle = scope.Resolve<IElevationMarkTextStyleGenerator>().Create<TextStyleTableRecord>();
            }
            // TextStyleTableRecord textStyle = ((TextStyleTableRecord)transaction.GetObject(acDoc.Database.Textstyle, OpenMode.ForRead));

        }

        protected abstract void Draw(Transaction _tr);

        public virtual void Run()
        {
            int osmode = Convert.ToInt16(Application.GetSystemVariable("Osmode"));
            int orthomode = Convert.ToInt16(Application.GetSystemVariable("Orthomode"));

            point = GetElevationPoint();
            texts = PrepareTextFields(point);
            placePoint = GetPlacePoint(point);

            ProxyCAD.UsingTransaction(Draw);

            Application.SetSystemVariable("Osmode", osmode);
            Application.SetSystemVariable("Orthomode", orthomode);
        }

        protected virtual Point3d GetElevationPoint()
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
            
            return pointResult.Value;
        }

        protected virtual Point3d GetPlacePoint(Point3d _point)
        {
            return _point;
        }

        protected virtual DBText[] PrepareTextFields(Point3d _point)
        {
            DBText[] texts = { new DBText(), new DBText() };
            texts[0].TextString = GetElevationSign(_point);
            texts[1].TextString = Math.Round(Math.Abs(_point.Y) * GetElevationUnitFactor(), 3).ToString("0.000", CultureInfo.GetCultureInfo("pl-PL"));
            var aaa = EntityInfo.GetTextArea(texts[1]);
            return texts;
        }

        protected void CheckPromptStatus(int osmode, int orthomode, PromptPointResult pointResult)
        {
            if (pointResult.Status == PromptStatus.Cancel || pointResult.Status == PromptStatus.None)
            {
                Application.SetSystemVariable("Osmode", osmode);
                Application.SetSystemVariable("Orthomode", orthomode);
                throw new ArgumentNullException();
            }
        }

        private void GetPoints()
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
            point = pointResult.Value;

            Application.SetSystemVariable("Osmode", 0);
            Application.SetSystemVariable("Orthomode", 0);

            promptOptions.Message = "\nWskaż kierunek koty wysokościowej: ";
            promptOptions.UseBasePoint = true;
            promptOptions.BasePoint = point;
            pointResult = ProxyCAD.Editor.GetPoint(promptOptions);
            CheckPromptStatus(osmode, orthomode, pointResult);
            placePoint = pointResult.Value;

            Application.SetSystemVariable("Osmode", osmode);
            Application.SetSystemVariable("Orthomode", orthomode);
        }

        private string GetElevationSign(Point3d _point)
        {
            if (Math.Round(Math.Abs(_point.Y) * GetElevationUnitFactor(), 3) == 0) 
            {
                return "%%p";
            }
            else if (_point.Y < 0)
            {
                return "-";
            }
            else
            {
                return "+";
            }
        }

        protected double GetElevationUnitFactor()
        {
            switch(AppSettings.Instance.DrawingUnit)
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
    }
}
