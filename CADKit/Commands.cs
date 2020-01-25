using CADKit.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit
{
    public static class Commands
    {
        [CommandMethod("CK_FLIPPALETE")]
        public static void FlipPalette()
        {
            AppSettings.Instance.FlipPalette();
        }
    }
}
