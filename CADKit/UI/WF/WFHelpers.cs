using System.Drawing;
using System.Windows.Forms;

namespace CADKit.UI.WF
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
