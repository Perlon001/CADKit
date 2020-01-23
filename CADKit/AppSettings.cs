using CADKit;
using CADKit.Extensions;
using CADKit.Models;
using CADKit.Proxy;
using CADKit.UI;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;

namespace CADKit
{
    public sealed class AppSettings
    {
        private static readonly AppSettings instance = new AppSettings();

        private CADKitPaletteSet palette;
        private Units drawingUnit;
        private Units dimensionUnit;
        private string drawingScale;

        static AppSettings() { }
        private AppSettings() 
        {
            AppPath = Path.Combine(Path.GetDirectoryName(this.GetType().Assembly.Location));

            GetSettingsFromDatabase();
            SetSettingsToDatabase();
        }

        public static AppSettings Instance { get { return instance; } }

        public string AppPath { get; private set; }

        public CADKitPaletteSet CADKitPalette 
        {
            get
            {
                if (palette == null)
                {
                    palette = new CADKitPaletteSet("CADKit", new Guid("53607c72-90e4-4bf8-b83d-c3da5a19c845"))
                    {
                        // TODO: Eliminacja zaleznosci do ZwSOFT.ZwCAD
                        // Visible must set to true before Dock settings
                        Visible = true,
                        Dock = ZwSoft.ZwCAD.Windows.DockSides.Left,
                        // TODO: Ustalenie minimalnego wymiaru palety
                        MinimumSize = new Size(450, 460),
                        KeepFocus = true
                    };
                    palette.PaletteSetDestroy += OnDestroy;
                    palette.StateChanged += OnStateChanged;

                }
                return palette;
            }
        }

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

        public void SetSettingsToDatabase()
        {
            CADProxy.SetCustomProperty("CKDrawingUnit", drawingUnit.ToString());
            CADProxy.SetCustomProperty("CKDimensionUnit", dimensionUnit.ToString());
            CADProxy.SetCustomProperty("CKDrawingScale", drawingScale);
        }

        public void GetSettingsFromDatabase()
        {
            drawingUnit = EnumsUtil.GetEnum(CADProxy.GetCustomProperty("CKDrawingUnit"), Units.mm);
            dimensionUnit = EnumsUtil.GetEnum(CADProxy.GetCustomProperty("CKDimensionUnit"), Units.mm);
            drawingScale = CADProxy.GetCustomProperty("CKDrawingScale");
            if(drawingScale == "")
            {
                DrawingScale = CADProxy.Database.Cannoscale.Name;
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

        private void OnDestroy(object sender, EventArgs e)
        {
            //CADProxy.Editor.WriteMessage("\nOnDestroj");
        }
        private void OnStateChanged(object sender, EventArgs e)
        {
            //CADProxy.Editor.WriteMessage("\nOnStateChanged");
        }
    }
}
