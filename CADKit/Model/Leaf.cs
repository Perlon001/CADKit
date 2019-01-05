using CADKit.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Model
{
    public class Leaf : IComponent
    {
        public string LeafName { get; set; }
        public string LeafTitle { get; set; }
        public string Layer { get; set; }
        public string Linetype { get; set; }
        public short ColorIndex { get; set; }
    }
}
