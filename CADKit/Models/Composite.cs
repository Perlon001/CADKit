using CADKit.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CADKit.Models
{
    public class Composite : IComposite
    {
        private readonly ICollection<IComponent> components = new List<IComponent>();

        public Composite(string _name)
        {
            Name = _name;
        }

        public string Name { get; protected set; }

        public string Title { get; set; }

        //public string Layer { get; set; }
        //public string Linetype { get; set; }
        //public short ColorIndex { get; set; }

        public IComponent Parent { get; set; }

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
