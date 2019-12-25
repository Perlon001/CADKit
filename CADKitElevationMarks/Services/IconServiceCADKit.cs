using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Models;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class IconServiceCADKit : IconService, IIconServiceCADKit
    {
        public IconServiceCADKit() : base()
        {
            icons.Add(MarkTypes.construction, null);
            icons.Add(MarkTypes.finish, null);
            icons.Add(MarkTypes.area, null);
        }
        public Bitmap GetIcon(MarkTypes _type)
        {
            return icons[_type];
        }
    }
}
