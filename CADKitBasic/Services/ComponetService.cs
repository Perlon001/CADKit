//using CADKit.Contracts;
//using CADKitBasic.Contracts.Services;
//using CADKitBasic.Models;
//using System.Collections.Generic;
//using System.Linq;

//namespace CADKitBasic.Services
//{
//    public class ComponetService : IComponentService
//    {
//        private readonly SortedSet<IComponent> components = null;

//        public ComponetService(IComponentProvider compositeProvider)
//        {
//            components = compositeProvider.GetModules();
//        }

//        public IList<string> GetAccessPath(IComponent composite)
//        {
//            List<string> path = new List<string>
//            {
//                composite.Name
//            };
//            while (composite.Parent != null)
//            {
//                composite = (IComposite)composite.Parent;
//                path.Add(composite.Name);
//            }
//            path.Reverse();

//            return path;
//        }

//        public IComposite GetComposite(string modulName, string compositeName)
//        {
//            var module = components.FirstOrDefault(a => a.Name == modulName);
//            if (module != null)
//            {
//                return (IComponent)module.GetComponent(compositeName);
//            }

//            return null;
//        }

//        public IComposite GetComposite(IComposite composite, string subCompositeName)
//        {
//            return (IComposite)composite.GetComponent(subCompositeName);
//        }

//        public ICollection<IComposite> GetComposites()
//        {
//            ICollection<IComposite> result = new List<IComposite>();

//            foreach (var item in components)
//            {
//                result.Add(item);
//            }

//            return result;
//        }

//        public ICollection<IComposite> GetComposites(string modulName)
//        {
//            ICollection<IComposite> result = new List<IComposite>();
//            var module = components.FirstOrDefault(a => a.Name == modulName);

//            foreach (var item in module.GetComponents())
//            {
//                result.Add((IComposite)item);
//            }

//            return result;
//        }

//        public ICollection<IComposite> GetComposites(IComposite composite)
//        {
//            ICollection<IComposite> result = new List<IComposite>();

//            foreach (var item in composite.GetComponents())
//            {
//                result.Add((IComposite)item);
//            }

//            return result;
//        }

//        public IDictionary<string, string> GetCompositeModulesList()
//        {
//            IDictionary<string, string> result = new Dictionary<string, string>();
//            foreach (var item in components)
//            {
//                result.Add(item.Name, item.Title);
//            }

//            return result;
//        }

//        public ICollection<IComponent> GetComponents()
//        {
//            throw new System.NotImplementedException();
//        }

//        public ICollection<IComponent> GetComponents(string modulName)
//        {
//            throw new System.NotImplementedException();
//        }

//        public ICollection<IComponent> GetComponents(IComponent composite)
//        {
//            throw new System.NotImplementedException();
//        }

//        public IComponent GetComponent(string modulName, string compositeName)
//        {
//            throw new System.NotImplementedException();
//        }

//        public IComponent GetComponent(IComponent composite, string subCompositeName)
//        {
//            throw new System.NotImplementedException();
//        }
//    }
//}
