using CADKit.Models;
using CADKitBasic.Models;
using CADKitElevationMarks.Models;
using System.Drawing;

namespace CADKitElevationMarks.Contracts.Services
{
    public interface IIconService
    {
        Bitmap GetIcon(MarkTypes type);
        Bitmap GetIcon(MarkTypes type, IconSize size);
    }
}
