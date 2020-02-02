using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if ZwCAD
using CADWindows = ZwSoft.ZwCAD.Windows;
#endif
#if AutoCAD
using CADWindows = Autodesk.AutoCAD.Windows;
#endif

namespace CADKit.Proxy.Windows
{
    public class Palette
    {
        private readonly static CADWindows.Palette palette;
        private PaletteSet paletteSet;
        public Palette(string name, PaletteSet palette)
        {

        }

        public PaletteSet PaletteSet
        {
            get { return this.paletteSet; }
            private set { paletteSet = value; }
        }
        public string Name { get { return palette.Name; } set { palette.Name = value; } }
    }
}
