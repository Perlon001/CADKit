using CADKitBasic.Contracts.Services;
using CADKit.Models;
using System.Collections.Generic;
using System.Linq;

namespace CADKitBasic.Services
{
    public class CompositeService : ICompositeService
    {
        private SortedSet<Composite> composites = null;

        public CompositeService(ICompositeProvider compositeProvider)
        {
            composites = compositeProvider.GetModules();
        }

        public IList<string> GetAccessPath(Composite composite)
        {
            List<string> path = new List<string>();
            path.Add(composite.Name);
            while (composite.Parent != null)
            {
                composite = (Composite)composite.Parent;
                path.Add(composite.Name);
            }
            path.Reverse();

            return path;
        }

        public Composite GetComposite(string modulName, string compositeName)
        {
            var module = composites.FirstOrDefault(a => a.Name == modulName);
            if (module != null)
            {
                return (Composite)module.GetComponent(compositeName);
            }

            return null;
        }

        public Composite GetComposite(Composite composite, string subCompositeName)
        {
            return (Composite)composite.GetComponent(subCompositeName);
        }

        public ICollection<Composite> GetComposites()
        {
            ICollection<Composite> result = new List<Composite>();

            foreach (var item in composites)
            {
                result.Add(item);
            }

            return result;
        }

        public ICollection<Composite> GetComposites(string modulName)
        {
            ICollection<Composite> result = new List<Composite>();
            var module = composites.FirstOrDefault(a => a.Name == modulName);

            foreach (var item in module.GetComponents())
            {
                result.Add((Composite)item);
            }

            return result;
        }

        public ICollection<Composite> GetComposites(Composite composite)
        {
            ICollection<Composite> result = new List<Composite>();

            foreach (var item in composite.GetComponents())
            {
                result.Add((Composite)item);
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
