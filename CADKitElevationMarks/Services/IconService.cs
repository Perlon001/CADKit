using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Services
{
    public abstract class IconService
    {
        protected Dictionary<MarkTypes, Bitmap> icons;

        public IconService()
        {
            icons = new Dictionary<MarkTypes, Bitmap>();
        }
    }
}
