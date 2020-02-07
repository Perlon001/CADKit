using CADKit.Contracts;
using CADKitElevationMarks.Models;
using System.Drawing;

namespace CADKitElevationMarks.Contracts.Services
{
    public interface IMarkIconService
    {
        Bitmap GetIcon(DrawingStandards standard, MarkTypes type);
        Bitmap GetIcon(DrawingStandards standard, MarkTypes type, IconSize size);
    }
}
