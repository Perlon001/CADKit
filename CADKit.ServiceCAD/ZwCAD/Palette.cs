using CADKit.ServiceCAD.Interface;
using System;
using System.Windows.Forms;

namespace CADKit.ServiceCAD.Proxy.ZwCAD
{
    public class Palette : ZwSoft.ZwCAD.Windows.PaletteSet, IPalette
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

        public new IPalette Add(string name, Control control)
        {
            base.Add(name, control);
            return this;
        }
    }
}
