using CADKit.Contracts;
using CADKitBasic.Models;
using System.Collections.Generic;

namespace CADKitBasic.Contracts.Services
{
    public interface IComponentService
    {
        IDictionary<string, string> GetCompositeModulesList();
        ICollection<IComponent> GetComponents();
        ICollection<IComponent> GetComponents(string modulName);
        ICollection<IComponent> GetComponents(IComponent composite);
        IComponent GetComponent(string modulName, string compositeName);
        IComponent GetComponent(IComponent composite, string subCompositeName);
        IList<string> GetAccessPath(IComponent composite);
    }
}
