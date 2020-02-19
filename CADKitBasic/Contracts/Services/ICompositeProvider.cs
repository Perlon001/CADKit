using CADKit.Contracts;
using System.Collections.Generic;

namespace CADKitBasic.Contracts.Services
{
    public interface ICompositeProvider
    {
        SortedSet<IComposite> GetModules();
    }
}
