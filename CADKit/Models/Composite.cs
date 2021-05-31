using CADKit.Contracts;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CADKit.Models
{
    public abstract class Composite : IComposite
    {
        protected readonly ICollection<IComponent> components = new List<IComponent>();

        protected Composite(string _name)
        {
            Name = _name;
        }

        public string Name { get; protected set; }
        public string Title { get; set; }
        public Image Image { get; set; }
        public object Tag { get; set; }
        public IComposite Parent { get; set; }

        public void AddComponent(IComponent _component)
        {
            _component.Parent = this;
            components.Add(_component);
        }

        public void RemoveComponent(IComponent _component)
        {
            components.Remove(_component);
        }

        public ICollection<IComponent> GetComponents()
        {
            return components;
        }

        public IComponent GetComponent(string _name)
        {
            return components.First(a => a.Name == _name);
        }

        public bool IsComposite { get { return true; } }

    }
}
