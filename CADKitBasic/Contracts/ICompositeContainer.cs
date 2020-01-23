using CADKitBasic.Models;
using System.Collections.Generic;

namespace CADKitBasic.Contracts
{
    public interface ICompositeContainer
    {
        void Register(string name, Composite composite);
        void Unregister(string name);

        IDictionary<string, Composite> GetLeafs();
    }

}
