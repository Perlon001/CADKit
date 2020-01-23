using CADKitBasic.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Services
{
    public class IconServiceStd02 : IconService, IIconServiceStd02
    {
        public override Bitmap GetIcon(MarkTypes _type, IconSize _size)
        {
            return DefaultIcon;
        }
    }
}
