using CADKit.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Model
{
    public class Composite : IComponent
    {
        private string leafName;
        private string leafTitle;
        private IList<IComponent> leafs = new List<IComponent>();
        
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

        public void AddLeaf(IComponent leaf)
        {
            leafs.Add(leaf);
        }

        public void RemoveLeaf(IComponent leaf)
        {
            leafs.Remove(leaf);
        }

        public IList<IComponent> GetLeafs()
        {
            return leafs;
        }

        public Composite GetLeaf(string name)
        {
            return (Composite)leafs.First(a => a.LeafName == name);
        }
    }
}
