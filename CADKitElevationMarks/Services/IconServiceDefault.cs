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
    public class IconServiceDefault : IconService, IIconServiceDefault
    {
        public override Bitmap GetIcon(MarkTypes type, IconSize size)
        {
            return Properties.Resources.question;
        }
    }
}
