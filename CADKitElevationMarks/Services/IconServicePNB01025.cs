using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Models;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class IconServicePNB01025 : IconService, IIconServicePNB01025
    {
        public IconServicePNB01025() : base()
        {
            icons.Add(MarkTypes.universal, null);
            icons.Add(MarkTypes.area, null);
        }
        public Bitmap GetIcon(MarkTypes _type)
        {
            return icons[_type];
        }
    }
}
