using CADKit.Contracts;
using CADKitBasic.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class IconServiceStd02 : IconService, IIconServiceStd02
    {
        public IconServiceStd02(IInterfaceSchemeService _service) : base(_service) { }

        protected override Bitmap GetIconForDarkScheme(MarkTypes type, IconSize size)
        {
            return DefaultIcon;
        }

        protected override Bitmap GetIconForLightScheme(MarkTypes type, IconSize size)
        {
            return DefaultIcon;
        }
    }
}
