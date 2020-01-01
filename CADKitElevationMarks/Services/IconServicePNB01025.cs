using CADKit.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using System;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class IconServicePNB01025 : IconService, IIconServicePNB01025
    {
        public override Bitmap GetIcon(MarkTypes _type, IconSize _size)
        {
            return Properties.Resources.mark01_32;
        }
    }
}
