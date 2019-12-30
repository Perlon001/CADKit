using CADKit.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public abstract class IconService : IIconService
    {
        public Bitmap GetIcon(MarkTypes _type)
        {
            return GetIcon(_type, IconSize.small);
        }

        public abstract Bitmap GetIcon(MarkTypes type, IconSize size);
    }
}
