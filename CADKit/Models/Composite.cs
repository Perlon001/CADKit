using CADKit.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace CADKit.Models
{
    public abstract class Composite : IComposite
    {
        protected readonly ICollection<IComposite> components = new List<IComposite>();

        protected Composite(string _name)
        {
            Name = _name;
        }

        public string Name { get; protected set; }

        public string Title { get; set; }

        public IComposite Parent { get; set; }

        public void AddComponent(IComposite _component)
        {
            _component.Parent = this;
            components.Add(_component);
        }

        public void RemoveComponent(IComposite _component)
        {
            components.Remove(_component);
        }

        public ICollection<IComposite> GetComponents()
        {
            return components;
        }

        public IComposite GetComponent(string _name)
        {
            return components.First(a => a.Name == _name);
        }

        public bool IsComposite { get { return true; } }
    }
}
