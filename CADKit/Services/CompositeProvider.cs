using CADKit.Contract.Services;
using CADKit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Services
{
    public abstract class CompositeProvider : ICompositeProvider
    {
        protected SortedSet<Composite> composites = null;

        public CompositeProvider()
        {
            composites = new SortedSet<Composite>(Comparer<Composite>.Create((x,y) => x.LeafTitle.CompareTo(y.LeafTitle)));
            Load();
        }

        public virtual SortedSet<Composite> GetModules()
        {
            return composites;
        }

        public abstract void Load();
    }
}
