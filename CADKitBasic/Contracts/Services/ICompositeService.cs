using CADKit.Contracts;
using System.Collections.Generic;

namespace CADKitBasic.Contracts.Services
{
    public interface ICompositeService
    {
        IDictionary<string, string> GetCompositeModulesList();
        ICollection<IComposite> GetComposites();
        ICollection<IComposite> GetComposites(string modulName);
        ICollection<IComposite> GetComposites(IComposite composite);
        IComposite GetComposite(string modulName, string compositeName);
        IComposite GetComposite(IComposite composite, string subCompositeName);
        IList<string> GetAccessPath(IComposite composite);
    }
}
