using CADKitBasic.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using System;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class IconServiceStd01 : IconService, IIconServiceStd01
    {
        public override Bitmap GetIcon(MarkTypes _type, IconSize _size)
        {
            switch (_type)
            {
                case MarkTypes.construction:
                    return Properties.Resources.mark04_32;
                case MarkTypes.finish:
                    return Properties.Resources.mark03_32;
                case MarkTypes.area:
                    return Properties.Resources.mark05_32;
            }

            return DefaultIcon;
        }
    }
}
