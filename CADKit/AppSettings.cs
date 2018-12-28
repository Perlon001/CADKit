using Autofac;
using CADKit.Contract;
using CADKit.DIContainer;
using CADKit.Model;
using CADKit.Presenters;
using CADKit.ServiceCAD;
using CADKit.ServiceCAD.Interface;
using CADKit.Util;
using System;
using System.Collections.Generic;

namespace CADKit
{
    public class AppSettings
    {
        private string environment;
        private DrawingStandards drawingStandard;
        private Units drawingUnit;
        private Units dimensionUnit;
        private string drawingScale;

        public string AppPath { get; private set; }
        public string AppName { get; private set; }
        public string Environment
        {
            get
            {
                return environment;
            }
            private set
            {
                environment = value;
            }
        }

        public DrawingStandards DrawingStandard
        {
            get
            {
                return drawingStandard;
            }
            set
            {
                drawingStandard = value;
                CADProxy.SetCustomProperty("CKDrawingStandard",drawingStandard.ToString());
            }
        }
        public Units DrawingUnit
        {
            get
            {
                return drawingUnit;
            }
            set
            {
                drawingUnit = value;
                CADProxy.SetCustomProperty("CKDrawingUnit", drawingUnit.ToString());
            }
        }
        public Units DimensionUnit
        {
            get
            {
                return dimensionUnit;
            }
            set
            {
                dimensionUnit = value;
                CADProxy.SetCustomProperty("CKDimensionUnit", dimensionUnit.ToString());
            }
        }
        public string DrawingScale
        {
            get
            {
                return drawingScale; 
            }
            set
            {
                drawingScale = value;
                CADProxy.SetCustomProperty("CKDrawingScale", drawingScale);
            }
        }
        public double ScaleFactor
        {
            get
            {
                double scale = CADProxy.Database.Cannoscale.Scale;
                switch (DrawingUnit)
                {
                    case Units.cm:
                        return 10 / scale;
                    case Units.m:
                        return 1000 / scale;
                    case Units.mm:
                        return 1 / scale;
                    default:
                        throw new Exception("Nie rozpoznana jednostka rysunkowa");
                }

            }
        }
        public IPalette CADKitPalette { get; set; }

        public void SetSettingsToDatabase()
        {
            CADProxy.SetCustomProperty("CKDrawingStandard", DrawingStandard.ToString());
            CADProxy.SetCustomProperty("CKDrawingUnit", DrawingUnit.ToString());
            CADProxy.SetCustomProperty("CKDimensionUnit", DimensionUnit.ToString());
            CADProxy.SetCustomProperty("CKDrawingScale", DrawingScale.ToString());
        }

        public void GetSettingsFromDatabase()
        {
            drawingStandard = EnumsUtil.GetEnum(CADProxy.GetCustomProperty("CKDrawingStandard"), DrawingStandards.PN_B_01025);
            drawingUnit = EnumsUtil.GetEnum(CADProxy.GetCustomProperty("CKDrawingUnit"), Units.mm);
            dimensionUnit = EnumsUtil.GetEnum(CADProxy.GetCustomProperty("CKDimensionUnit"), Units.mm);
            drawingScale = CADProxy.GetCustomProperty("CKDrawingScale");
        }

        public void Reset()
        {
            CADKitPalette.Visible = false;
            DrawingScale = "";
        }

        public AppSettings()
        {

            AppPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location));
            AppName = this.GetType().Assembly.GetName().Name;
            CADKitPalette = CADProxy.GetEntityFactory().GetPalette(AppName);
     
            GetSettingsFromDatabase();
            SetSettingsToDatabase();

            //TextHigh = new Dictionary<TextStyles, double>()
            //{
            //    { TextStyles.verysmall, 1.50 },
            //    { TextStyles.small,     1.75 },
            //    { TextStyles.normal,    2.00 },
            //    { TextStyles.medium,    4.00 },
            //    { TextStyles.big,       8.00 },
            //    { TextStyles.verybig,  12.00 },
            //    { TextStyles.dim,       2.00 },
            //    { TextStyles.elevmark,  2.00 },
            //};
        }
    }
}
