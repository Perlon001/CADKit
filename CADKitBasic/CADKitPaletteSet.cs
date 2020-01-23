using CADKit.Contracts;
using CADProxy.Models;
using System;
using System.Collections.Generic;

namespace CADKit
{
    public class CADKitPaletteSet : PaletteSet
    {
        public ICollection<IPalettePage> palettePages { get; set; }

        public CADKitPaletteSet(string name) : base(name)
        {
        }

        public CADKitPaletteSet(string name, Guid toolID) : base(name, toolID)
        {
        }

        public CADKitPaletteSet(string name, string cmd, Guid toolID) : base(name, cmd, toolID)
        {
        }

        public bool PaletteState { get; protected set; }

        public void AddPage(IPalettePage page)
        {
            AddPage(page, palettePages.Count);
        }

        public void AddPage(IPalettePage page, int posAfter)
        {
            palettePages.Add(page);
        }

    }
}
