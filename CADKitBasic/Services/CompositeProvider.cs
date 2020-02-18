using CADKitBasic.Contracts.Services;
using CADKit.Models;
using System.Collections.Generic;

namespace CADKitBasic.Services
{
    public abstract class CompositeProvider : ICompositeProvider
    {
        protected SortedSet<Composite> composites = null;

        public CompositeProvider()
        {
            composites = new SortedSet<Composite>(Comparer<Composite>.Create((x,y) => x.Title.CompareTo(y.Title)));
            Load();
        }

        public virtual SortedSet<Composite> GetModules()
        {
            return composites;
        }

        public abstract void Load();
    }
}
