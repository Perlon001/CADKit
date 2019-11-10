using CADKit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Contracts.Services
{
    public interface ICompositeService
    {
        IDictionary<string, string> GetCompositeModulesList();
        ICollection<Composite> GetComposites();
        ICollection<Composite> GetComposites(string modulName);
        ICollection<Composite> GetComposites(Composite composite);
        Composite GetComposite(string modulName, string compositeName);
        Composite GetComposite(Composite composite, string subCompositeName);
        IList<string> GetAccessPath(Composite composite);
    }
}
