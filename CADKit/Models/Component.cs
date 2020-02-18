using CADKit.Contracts;

namespace CADKit.Models
{
    public class Component : IComponent
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Layer { get; set; }
        public string Linetype { get; set; }
        public short ColorIndex { get; set; }
        public IComponent Parent { get; set; }
        public bool IsComposite { get { return false; } }
    }
}
