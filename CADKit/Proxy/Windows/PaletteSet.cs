using System;

#if ZwCAD
using CADWindows = ZwSoft.ZwCAD.Windows;
#endif
#if AutoCAD
using CADWindows = Autodesk.AutoCAD.Windows;
#endif

namespace CADKit.Proxy.Windows
{
    public class PaletteSet : CADWindows.PaletteSet
    {
     
        public PaletteSet(string name) : base(name)
        {
        }

        public PaletteSet(string name, Guid toolID) : base(name, toolID)
        {
        }

        public PaletteSet(string name, string cmd, Guid toolID) : base(name, cmd, toolID)
        {
        }


    }
}
