using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#if ZwCAD
using ZwSoft.ZwCAD.Windows;
#endif
#if AutoCAD
using Autodesk.AutoCAD.Windows;
#endif

namespace CADKit.ServiceCAD.Proxy
{
    public class Palette : PaletteSet
    {
        public Palette(string name) : base(name)
        {
        }

        public Palette(string name, Guid toolID) : base(name, toolID)
        {
        }

        public Palette(string name, string cmd, Guid toolID) : base(name, cmd, toolID)
        {
        }
    }
}
