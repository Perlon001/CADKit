using CADKit.Contracts.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Contracts
{
    public interface IPalettePage
    {
        string Title { get; set; }
        IView View { get; set; }
    }
}
