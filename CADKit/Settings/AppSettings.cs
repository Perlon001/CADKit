using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitCore.Settings
{
    public sealed class AppSettings
    {

        public string AppPath { get; private set; }
        public string AppName { get; private set; }

        public DrawingStandards DrawingStandard { get; set; }
        public DrawingUnits DrawingUnit { get; set; }
        public double DrawingScale { get; set; }
        public Dictionary<TextStyles, double> TextHigh { get; set; }
        public double ScaleFactor
        {
            get
            {
                switch (DrawingUnit)
                {
                    case DrawingUnits.cm:
                        return 10 / DrawingScale;
                    case DrawingUnits.m:
                        return 1000 / DrawingScale;
                    case DrawingUnits.mm:
                        return 1 / DrawingScale;
                    default:
                        throw new Exception("Nie rozpoznana jednostka rysunkowa");
                }

            }
        }

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
                }
                return instance;
            }
        }

        private AppSettings()
        {
            AppPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location));
            AppName = this.GetType().Assembly.GetName().Name;
            DrawingUnit = DrawingUnits.mm;
            DrawingScale = 0.01;
            DrawingStandard = DrawingStandards.PN_B_01025;
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
        }
    }
}
