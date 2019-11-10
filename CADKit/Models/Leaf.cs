using CADKit.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Models
{
    public class Leaf : IComponent
    {
        public string LeafName { get; set; }
        public string LeafTitle { get; set; }
        public string Layer { get; set; }
        public string Linetype { get; set; }
        public short ColorIndex { get; set; }
        public IComponent Parent { get; set; }
    }
}
