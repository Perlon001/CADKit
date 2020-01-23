using CADKitBasic.Contracts;

namespace CADKitBasic.Models
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
