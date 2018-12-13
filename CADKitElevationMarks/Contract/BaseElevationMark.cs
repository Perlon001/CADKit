using Autofac;
using CADKitCore.Settings;
using CADKitCore.Util;
using System;
using System.Globalization;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;

namespace CADKitElevationMarks.Contract
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
        protected Point3d directionPoint;

        public BaseElevationMark(IElevationMarkConfig _config)
        {
            this.config = _config;
            this.scaleFactor = AppSettings.Instance.ScaleFactor;
            this.texts = new DBText[2];
            this.ucs = Application.DocumentManager.MdiActiveDocument.Editor.CurrentUserCoordinateSystem;
            this.coordinateSystem = Application.DocumentManager.MdiActiveDocument.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d;
            this.transformMatrix = Matrix3d.AlignCoordinateSystem(
                Point3d.Origin,
                Vector3d.XAxis,
                Vector3d.YAxis,
                Vector3d.ZAxis,
                coordinateSystem.Origin,
                coordinateSystem.Xaxis,
                coordinateSystem.Yaxis,
                coordinateSystem.Zaxis);
            Document acDoc = Application.DocumentManager.MdiActiveDocument;
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                acDoc.Database.Textstyle = scope.Resolve<IElevationMarkTextStyleGenerator>().Generate<TextStyleTableRecord>();
                acDoc.Database.Clayer = scope.Resolve<IElevationMarkLayerGenerator>().Generate<LayerTableRecord>();
            }
            // TextStyleTableRecord textStyle = ((TextStyleTableRecord)transaction.GetObject(acDoc.Database.Textstyle, OpenMode.ForRead));

        }

        protected abstract void Draw();

        public virtual void Create()
        {
            GetPoints();
            PrepareTextFields();
            Draw();
        }

        private void PrepareTextFields()
        {
            texts[0] = new DBText();
            texts[1] = new DBText();
            texts[0].TextString = GetSignValue();
            texts[1].TextString = Math.Round(Math.Abs(point.Y) * GetElevationFactor(), 3).ToString("0.000", CultureInfo.GetCultureInfo("pl-PL"));
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

        protected virtual void GetPoints()
        {
            PromptPointResult pointResult;
            PromptPointOptions promptOptions;
            int osmode = Convert.ToInt16(Application.GetSystemVariable("Osmode"));
            int orthomode = Convert.ToInt16(Application.GetSystemVariable("Orthomode"));

            Document acDoc = Application.DocumentManager.MdiActiveDocument;

            Application.SetSystemVariable("Osmode", 512);

            promptOptions = new PromptPointOptions("\nWskaż punkt wysokościowy: ");
            promptOptions.AllowNone = true;
            pointResult = acDoc.Editor.GetPoint(promptOptions);
            CheckPromptStatus(osmode, orthomode, pointResult);
            point = pointResult.Value;

            Application.SetSystemVariable("Osmode", 0);
            Application.SetSystemVariable("Orthomode", 0);

            promptOptions.Message = "\nWskaż kierunek koty wysokościowej: ";
            promptOptions.UseBasePoint = true;
            promptOptions.BasePoint = point;
            pointResult = acDoc.Editor.GetPoint(promptOptions);
            CheckPromptStatus(osmode, orthomode, pointResult);
            directionPoint = pointResult.Value;

            Application.SetSystemVariable("Osmode", osmode);
            Application.SetSystemVariable("Orthomode", orthomode);
        }

        private string GetSignValue()
        {
            if (Math.Round(Math.Abs(point.Y) * GetElevationFactor(), 3) == 0) 
            {
                return "%%p";
            }
            else if (point.Y < 0)
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
                case DrawingUnits.m:
                    return 1;
                case DrawingUnits.cm:
                    return 0.01;
                case DrawingUnits.mm:
                    return 0.001;
                default:
                    throw new Exception("\nNie rozpoznana jednostka rysunkowa");
            }
        }
    }
}
