using CADKit.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using System;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class IconServiceCADKit : IconService, IIconServiceCADKit
    {
        public override Bitmap GetIcon(MarkTypes type, IconSize size)
        {
            throw new NotImplementedException();
        }
    }
}
