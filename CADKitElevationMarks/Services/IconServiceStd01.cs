using CADKit.Contracts;
using CADKitBasic.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class IconServiceStd01 : IconService, IIconServiceStd01
    {
        public IconServiceStd01(IInterfaceSchemeService _service) : base(_service) { }

        internal override Bitmap GetIconForLightScheme(MarkTypes _type, IconSize _size)
        {
            Bitmap result = DefaultIcon;

            switch (_type)
            {
                case MarkTypes.construction:
                    result = Properties.Resources.mark04_32;
                    break;
                case MarkTypes.finish:
                    result = Properties.Resources.mark03_32;
                    break;
                case MarkTypes.area:
                    result = Properties.Resources.mark05_32;
                    break;
            }

            return result;

        }
        internal override Bitmap GetIconForDarkScheme(MarkTypes _type, IconSize _size)
        {
            Bitmap result = DefaultIcon;

            switch (_type)
            {
                case MarkTypes.construction:
                    result = Properties.Resources.mark04_32_dark;
                    break;
                case MarkTypes.finish:
                    result = Properties.Resources.mark03_32_dark;
                    break;
                case MarkTypes.area:
                    result = Properties.Resources.mark05_32_dark;
                    break;
            }

            return result;
        }
    }
}
