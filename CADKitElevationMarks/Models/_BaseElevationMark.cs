using Autofac;
using CADKit;
using CADKit.Models;
using CADKitElevationMarks.Contracts;
using System;
using System.Globalization;
using CADProxy;
using System.Collections.Generic;

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
    public abstract class _BaseElevationMark
    {
        //protected readonly Matrix3d ucs = ProxyCAD.Editor.CurrentUserCoordinateSystem;
        //protected readonly CoordinateSystem3d coordinateSystem = ProxyCAD.Editor.CurrentUserCoordinateSystem.CoordinateSystem3d;
        //protected readonly double scaleFactor = AppSettings.Instance.ScaleFactor;
        //protected readonly Matrix3d transformMatrix;
        //protected readonly Editor ed = ProxyCAD.Editor;
        //protected readonly Database db = ProxyCAD.Database;


        //protected DBText[] texts;
        protected Point3d elevationPoint;
        //protected Point3d directionPoint;
        //protected JigDisplacement jig;
        //protected IEnumerable<Entity> markEntities;

        protected string sign;
        protected string value;
        protected readonly IElevationMarkConfig config;

        public string Sign
        {
            get
            {
                return this.sign;
            }
            protected set
            {
                if (value == null) throw new ArgumentNullException("Znak nie może być pusty.");
                if (this.sign != null) throw new InvalidOperationException("Znak jest już zdefiniowany.");
                this.sign = value;
            }
        }

        public string Value
        {
            get
            {
                return this.value;
            }
            protected set
            {
                if (value == null) throw new ArgumentNullException("Wartość nie może być pusta.");
                if (this.value != null) throw new InvalidOperationException("Wartość jest już zdefiniowana.");
                this.value = value;
            }
        }

        public IEnumerable<Entity> EntityList => throw new NotImplementedException();

        public _BaseElevationMark()
        {

        }

        public _BaseElevationMark(IElevationMarkConfig _config)
        {
            this.config = _config;
            //this.texts = new DBText[] { new DBText(), new DBText() };

            //this.transformMatrix = Matrix3d.AlignCoordinateSystem(
            //    Point3d.Origin,
            //    Vector3d.XAxis,
            //    Vector3d.YAxis,
            //    Vector3d.ZAxis,
            //    coordinateSystem.Origin,
            //    coordinateSystem.Xaxis,
            //    coordinateSystem.Yaxis,
            //    coordinateSystem.Zaxis);

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

        protected abstract Group DrawEntities(Transaction tr);

        protected abstract IEnumerable<Entity> GetEntities();

        public abstract void Draw();
 
        protected void GetElevationPoint()
        {
            PromptPointOptions promptOptions = new PromptPointOptions("\nWskaż punkt wysokościowy: ");
            promptOptions.AllowNone = true;
            int osmode = Convert.ToInt16(Application.GetSystemVariable("Osmode"));
            Application.SetSystemVariable("Osmode", 512);
            PromptPointResult pointResult = ProxyCAD.Editor.GetPoint(promptOptions);
            Application.SetSystemVariable("Osmode", osmode);
            if (pointResult.Status == PromptStatus.Cancel || pointResult.Status == PromptStatus.None)
            {
                throw new ArgumentNullException();
            }
            //elevationPoint = pointResult.Value;
        }

        protected void GetDirectionPoint()
        {
            // directionPoint = elevationPoint;
        }

        protected virtual void PrepareTextFields()
        {
            //this.texts[0].TextString = GetElevationSign();
            //this.texts[1].TextString = Math.Round(Math.Abs(elevationPoint.Y) * GetElevationFactor(), 3).ToString("0.000", CultureInfo.GetCultureInfo("pl-PL"));
        }

        protected string GetElevationSign()
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

        public abstract IEnumerable<Entity> GetEntity();
    }
}
