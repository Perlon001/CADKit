using CADKit;
using CADKit.Models;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Contracts.Services
{
    public interface IIconService
    {
        Bitmap GetIcon(MarkTypes type);
        Bitmap GetIcon(MarkTypes type, IconSize size);

    }
}
