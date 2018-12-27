using CADKitCore.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using CADKitCore.Views;
using CADKitCore.Contract;
using CADKitCore.Presenters;
using CADKitCore.Views.WF;
using CADKit.ServiceCAD;

namespace CADKitCore.Settings
{
    public sealed class AppSettings
    {
        private string environment;
        private double drawingScale;

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

        public DrawingStandards DrawingStandard { get; set; }
        public Units DrawingUnit { get; set; }
        public Units DimensionUnit { get; set; }
        public double DrawingScale
        {
            get
            {
                return drawingScale;
            }
            set
            {
                drawingScale = value;
                CADProxy.Database.SetCustomProperty("CKDrawingScale", drawingScale.ToString());
            }
        }
        public Dictionary<TextStyles, double> TextHigh { get; set; }
        public double ScaleFactor
        {
            get
            {
                switch (DrawingUnit)
                {
                    case Units.cm:
                        return 10 / DrawingScale;
                    case Units.m:
                        return 1000 / DrawingScale;
                    case Units.mm:
                        return 1 / DrawingScale;
                    default:
                        throw new Exception("Nie rozpoznana jednostka rysunkowa");
                }

            }
        }
        public CADKitPalette CADKitPalette { get; set; }

        //public Dictionary<ObjectTypes, string> DefaultLayers { get; set; }
        //public Dictionary<string, Color> DefaultLayerColors { get; set; }
        //public Dictionary<ObjectTypes, Color> DefaultObjectColors { get; set; }
        // ...
        // ...

        private static AppSettings instance = null;

        public static AppSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppSettings();
                    ISettingsView settingsView = new SettingsView();
                    ISettingsPresenter settingsPresenter = new SettingsPresenter(settingsView);
                    AppSettings.Instance.CADKitPalette.Add("Ustawienia", settingsView as Control);
                    AppSettings.Instance.CADKitPalette.Visible = true;
                }
                return instance;
            }
        }

        public void SetSettingsToDatabase()
        {
            CADProxy.Database.SetCustomProperty("CKDrawingStandard", DrawingStandard.ToString());
            CADProxy.Database.SetCustomProperty("CKDrawingUnit", DrawingUnit.ToString());
            CADProxy.Database.SetCustomProperty("CKDrawingScale", DrawingScale.ToString());
        }

        public void GetSettingsFromDatabase()
        {
            DrawingStandard = EnumsUtil.GetEnum(CADProxy.Database.GetCustomProperty("CKDrawingStandard"), DrawingStandards.PN_B_01025);
            DrawingUnit = EnumsUtil.GetEnum(CADProxy.Database.GetCustomProperty("CKDrawingUnit"), Units.mm);
            try
            {
                drawingScale = Convert.ToDouble(CADProxy.Database.GetCustomProperty("CKDrawingScale"));
            }
            catch (FormatException)
            {
                drawingScale = 0.01;
            }
        }

        private AppSettings()
        {
            AppPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location));
            AppName = this.GetType().Assembly.GetName().Name;
            GetSettingsFromDatabase();
            TextHigh = new Dictionary<TextStyles, double>()
            {
                { TextStyles.verysmall, 1.50 },
                { TextStyles.small,     1.75 },
                { TextStyles.normal,    2.00 },
                { TextStyles.medium,    4.00 },
                { TextStyles.big,       8.00 },
                { TextStyles.verybig,  12.00 },
                { TextStyles.dim,       2.00 },
                { TextStyles.elevmark,  2.00 },
            };
            SetSettingsToDatabase();
            CADKitPalette = new CADKitPalette(AppName);
            CADKitPalette.MinimumSize = new Size(250, 250);
        }

    }
}
