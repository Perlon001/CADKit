using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CADKitElevationMarks.Views
{
    public static class WFHelpers
    {
        public static void ChangeColorSchema(this Control control, Color foreColor, Color backColor)
        {
            control.ForeColor = foreColor;
            control.BackColor = backColor;
        }
    }
}
