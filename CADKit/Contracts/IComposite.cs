using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Contracts
{
    public interface IComposite : IComponent
    {
        void AddComponent(IComposite component);
        void RemoveComponent(IComposite component);
        ICollection<IComposite> GetComponents();
        IComposite GetComponent(string name);
    }
}
