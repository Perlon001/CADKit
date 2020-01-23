using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.ServiceCAD.Interface
{
    public interface IPalette
    {
        bool Visible { get; set; }
        IPalette Add(string name, System.Windows.Forms.Control control);
    }
}
