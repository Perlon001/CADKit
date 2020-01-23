using CADKit.Models;
using System.Collections.Generic;

namespace CADKit.Contracts.Services
{
    public interface ICompositeProvider
    {
        SortedSet<Composite> GetModules();
    }
}
