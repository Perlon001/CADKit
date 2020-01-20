using CADKit.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CADKit.Models
{
    public class Composite : IComponent
    {
        private string leafName;
        private string leafTitle;
        private ICollection<IComponent> leafs = new List<IComponent>();
        private Composite parent;

        public string LeafName
        {
            get { return leafName; }
            set
            {
                string tmp = value.Trim().ToLower();
                if (tmp.IndexOf(" ") == 0)
                {
                    throw new ArgumentException("Nieprawidłowy argument. Nazwa nie może zawierać spacji.");
                }
                leafName = value;
            }
        }
        public string LeafTitle
        {
            get { return leafTitle; }
            set { leafTitle = value.Trim(); }
        }
        public string Layer { get; set; }
        public string Linetype { get; set; }
        public short ColorIndex { get; set; }
        public IComponent Parent { get; set; }

        public void AddLeaf(IComponent leaf)
        {
            leaf.Parent = this;
            leafs.Add(leaf);
        }

        public void RemoveLeaf(IComponent leaf)
        {
            leafs.Remove(leaf);
        }

        public ICollection<IComponent> GetLeafs()
        {
            return leafs;
        }

        public IComponent GetLeaf(string name)
        {
            return leafs.First(a => a.LeafName == name);
        }
    }
}
