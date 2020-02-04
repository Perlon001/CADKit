using CADKit.Contracts;
using CADKitBasic.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class IconServicePNB01025 : IconService, IIconServicePNB01025
    {
        public IconServicePNB01025(IInterfaceSchemeService _service) : base(_service) { }

        internal override Bitmap GetIconForLightScheme(MarkTypes _type, IconSize _size)
        {
            Bitmap result = DefaultIcon;

            switch (_type)
            {
                case MarkTypes.universal:
                    result = Properties.Resources.mark01_32;
                    break;
                case MarkTypes.area:
                    result = Properties.Resources.mark02_32;
                    break;
            }

            return result;

        }
        internal override Bitmap GetIconForDarkScheme(MarkTypes _type, IconSize _size)
        {
            Bitmap result = DefaultIcon;

            switch (_type)
            {
                case MarkTypes.universal:
                    result = Properties.Resources.mark01_32_dark;
                    break;
                case MarkTypes.area:
                    result = Properties.Resources.mark02_32_dark;
                    break;
            }

            return result;
        }
    }
}
