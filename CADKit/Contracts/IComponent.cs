using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Contracts
{
    public interface IComponent
    {
        string LeafName { get; set; }
        string LeafTitle { get; set; }
        string Layer { get; set; }
        string Linetype { get; set; }
        short ColorIndex { get; set; }
        IComponent Parent { get; set; }
    }
}
