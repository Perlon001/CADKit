using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Services
{
    public class IconServiceCADKit : IconService, IIconServiceCADKit
    {
        public IconServiceCADKit() : base()
        {
            icons.Add(MarkTypes.construction, null);
            icons.Add(MarkTypes.finish, null);
            icons.Add(MarkTypes.plane, null);
        }
        public Bitmap GetIcon(MarkTypes _type)
        {
            return icons[_type];
        }
    }
}
