using CADKitBasic.Contracts.Services;
using System.Collections.Generic;
using CADKit.Contracts;

namespace CADKitBasic.Services
{
    public abstract class CompositeProvider : ICompositeProvider
    {
        protected SortedSet<IComposite> composites = null;

        public CompositeProvider()
        {
            composites = new SortedSet<IComposite>(Comparer<IComposite>.Create((x,y) => x.Title.CompareTo(y.Title)));
            Load();
        }

        public virtual SortedSet<IComposite> GetModules()
        {
            return composites;
        }

        public abstract void Load();
    }
}
