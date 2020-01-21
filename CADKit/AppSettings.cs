using CADKit.Models;
using CADKit.Utils;
using CADProxy;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;

namespace CADKit
{
    public sealed class AppSettings
    {
        private static readonly AppSettings instance = new AppSettings();
        static AppSettings() { }
        private AppSettings() 
        {
            AppPath = Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location));
            CADKitPalette = new CADKitPaletteSet(AppName);
            // TODO: Nalezy usunąć zależność od ZwSoft.ZwCAD.Windows
            //CADKitPalette.Dock = ZwSoft.ZwCAD.Windows.DockSides.Left;
            //CADKitPalette.DockEnabled = ZwSoft.ZwCAD.Windows.DockSides.Left;
            //CADKitPalette.SizeChanged -= OnResize;
            CADKitPalette.SizeChanged += OnResize;
            //CADKitPalette.Load += OnLoad;
            GetSettingsFromDatabase();
            SetSettingsToDatabase();
        }
        private DrawingStandards drawingStandard;
        private Units drawingUnit;
        private Units dimensionUnit;
        private string drawingScale;
        public static AppSettings Instance { get { return instance; } }

        public string AppPath { get; private set; }

        public const string AppName = "CADKit";

        public CADKitPaletteSet CADKitPalette { get; set; }

        public InterfaceScheme ColorScheme 
        {
            get
            {
                var schemeValue = (int)Registry.CurrentUser.OpenSubKey("Software\\ZWSOFT\\ZWCAD\\2020\\en-US\\Profiles\\Default\\Config\\COLORSCHEME", false).GetValue("COLORSCHEME");
                switch (schemeValue)
                {
                    case 1:
                        return InterfaceScheme.light;
                    default:
                        return InterfaceScheme.dark;
                }
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
                ProxyCAD.SetCustomProperty("CKDrawingStandard", drawingStandard.ToString());
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
                ProxyCAD.SetCustomProperty("CKDrawingUnit", drawingUnit.ToString());
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
                ProxyCAD.SetCustomProperty("CKDimensionUnit", dimensionUnit.ToString());
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
                ProxyCAD.SetCustomProperty("CKDrawingScale", drawingScale);
            }
        }
        public double ScaleFactor
        {
            get
            {
                double scale = ProxyCAD.Database.Cannoscale.Scale;
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

        public void SetSettingsToDatabase()
        {
            ProxyCAD.SetCustomProperty("CKDrawingStandard", drawingStandard.ToString());
            ProxyCAD.SetCustomProperty("CKDrawingUnit", drawingUnit.ToString());
            ProxyCAD.SetCustomProperty("CKDimensionUnit", dimensionUnit.ToString());
            ProxyCAD.SetCustomProperty("CKDrawingScale", drawingScale);
        }

        public void GetSettingsFromDatabase()
        {
            drawingStandard = EnumsUtil.GetEnum(ProxyCAD.GetCustomProperty("CKDrawingStandard"), DrawingStandards.PNB01025);
            drawingUnit = EnumsUtil.GetEnum(ProxyCAD.GetCustomProperty("CKDrawingUnit"), Units.mm);
            dimensionUnit = EnumsUtil.GetEnum(ProxyCAD.GetCustomProperty("CKDimensionUnit"), Units.mm);
            drawingScale = ProxyCAD.GetCustomProperty("CKDrawingScale");
            if(drawingScale == "")
            {
                DrawingScale = ProxyCAD.Database.Cannoscale.Name;
            }
        }

        public void Reset()
        {
            CADKitPalette.Visible = false;
            drawingScale = "";
        }

        private void OnResize(object sender, EventArgs e)
        {
            CADKitPalette.Name = "CADKit " + CADKitPalette.Size.Width;
        }
    }
}
