using CADKit.Contracts;
using CADKitBasic.Models;
using System.Collections.Generic;

namespace CADKitBasic.Contracts.Services
{
    public interface IComponentProvider
    {
        SortedSet<IComponent> GetModules();
    }
}
