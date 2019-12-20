using CADKit;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Contracts
{
    public interface IIconService
    {
        Bitmap GetIcon(MarkTypes type);
    }
}
