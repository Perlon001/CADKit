using CADKit.Contract;
using CADKit.Contract.Services;
using CADKit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Util
{
    /// <summary>
    /// Composite container contains a dictionary of composits
    /// </summary>
    public abstract class CompositeContainer : ICompositeContainer
    {
        protected IDictionary<string, Composite> container;

        public CompositeContainer(ICompositeService compositeService)
        {
            container = compositeService.LoadComposites();
        }

        /// <summary>
        /// Zwraca Dictionary string Composites zawierający wszystkie zarejestrowane kompozyty, których mozna użyć w aplikacji
        /// </summary>
        /// <returns></returns>
        public virtual IDictionary<string, Composite> GetLeafs()
        {
            return container;
        }

        public virtual void Register(string name, Composite composite)
        {
            container.Add(name, composite);
        }

        public virtual void Unregister(string name)
        {
            container.Remove(name);
        }
    }
}
