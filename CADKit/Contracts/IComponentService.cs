using System.Collections.Generic;

namespace CADKit.Contracts
{
    public interface IComponentService
    {
        ICollection<IComponent> GetComponents();
    }
}
