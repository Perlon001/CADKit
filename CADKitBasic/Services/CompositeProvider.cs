using CADKitBasic.Contracts.Services;
using CADKitBasic.Models;
using System.Collections.Generic;

namespace CADKitBasic.Services
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
