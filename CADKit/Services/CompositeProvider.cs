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
        protected SortedDictionary<string, Composite> composites;

        public CompositeProvider(ICompositeService compositeService)
        {
            composites = compositeService.LoadComposites();
        }

        public Composite GetComposite(string module, string compositeName)
        {
            Composite leaf = composites[module];

            return leaf.GetLeaf(compositeName);
        }
    }
}
