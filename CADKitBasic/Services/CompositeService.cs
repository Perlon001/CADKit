using CADKitBasic.Contracts.Services;
using CADKit.Models;
using System.Collections.Generic;
using System.Linq;
using CADKit.Contracts;

namespace CADKitBasic.Services
{
    public class CompositeService : ICompositeService
    {
        private SortedSet<IComposite> composites = null;

        public CompositeService(ICompositeProvider compositeProvider)
        {
            composites = compositeProvider.GetModules();
        }

        public IList<string> GetAccessPath(IComposite composite)
        {
            List<string> path = new List<string>();
            path.Add(composite.Name);
            while (composite.Parent != null)
            {
                composite = (IComposite)composite.Parent;
                path.Add(composite.Name);
            }
            path.Reverse();

            return path;
        }

        public IComposite GetComposite(string modulName, string compositeName)
        {
            var module = composites.FirstOrDefault(a => a.Name == modulName);
            if (module != null)
            {
                return (IComposite)module.GetComponent(compositeName);
            }

            return null;
        }

        public IComposite GetComposite(IComposite composite, string subCompositeName)
        {
            return (IComposite)composite.GetComponent(subCompositeName);
        }

        public ICollection<IComposite> GetComposites()
        {
            ICollection<IComposite> result = new List<IComposite>();

            foreach (var item in composites)
            {
                result.Add(item);
            }

            return result;
        }

        public ICollection<IComposite> GetComposites(string modulName)
        {
            ICollection<IComposite> result = new List<IComposite>();
            var module = composites.FirstOrDefault(a => a.Name == modulName);

            foreach (var item in module.GetComponents())
            {
                result.Add((IComposite)item);
            }

            return result;
        }

        public ICollection<IComposite> GetComposites(IComposite composite)
        {
            ICollection<IComposite> result = new List<IComposite>();

            foreach (var item in composite.GetComponents())
            {
                result.Add((IComposite)item);
            }

            return result;
        }

        public IDictionary<string, string> GetCompositeModulesList()
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in composites)
            {
                result.Add(item.Name, item.Title);
            }

            return result;
        }


    }
}
