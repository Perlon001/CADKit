using CADKit.Models;
using System.Collections.Generic;

namespace CADKit.Contracts
{
    public interface ICompositeContainer
    {
        void Register(string name, Composite composite);
        void Unregister(string name);

        IDictionary<string, Composite> GetLeafs();
    }

}
