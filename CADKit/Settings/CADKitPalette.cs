using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.Windows;

namespace CADKitCore.Settings
{
    public class CADKitPalette : PaletteSet
    {
        public CADKitPalette(string name) : base(name)
        {
        }

        public CADKitPalette(string name, Guid toolID) : base(name, toolID)
        {
        }

        public CADKitPalette(string name, string cmd, Guid toolID) : base(name, cmd, toolID)
        {
        }
    }
}
