using CADKitBasic.Models;
using System.Collections.Generic;

namespace CADKitBasic.Contracts.Services
{
    public interface ICompositeProvider
    {
        SortedSet<Composite> GetModules();
    }
}
