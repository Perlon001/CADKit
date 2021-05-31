using CADKit.Contracts;
using System.Drawing;

namespace CADKit.Models
{
    public abstract class Component : IComponent
    {
        protected Component(string _name)
        {
            Name = _name;
        }

        public string Name { get; protected set; }
        public string Title { get; set; }
        public Image Image { get; set; }
        public object Tag { get; set; }
        public IComposite Parent { get; set; }
        public bool IsComposite { get { return false; } }
    }
}
