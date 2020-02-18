using CADKit.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CADKit.Models
{
    public class Composite : IComponent
    {
        private string name;
        private string title;
        private readonly ICollection<IComponent> components = new List<IComponent>();

        public string Name
        {
            get { return name; }
            set
            {
                string tmp = value.Trim().ToLower();
                if (tmp.IndexOf(" ") == 0)
                {
                    throw new ArgumentException("Nieprawidłowy argument. Nazwa nie może zawierać spacji.");
                }
                name = value;
            }
        }

        public string Title
        {
            get { return title; }
            set { title = value.Trim(); }
        }
        public string Layer { get; set; }
        public string Linetype { get; set; }
        public short ColorIndex { get; set; }
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
