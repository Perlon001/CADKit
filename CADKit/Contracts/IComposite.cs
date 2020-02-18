using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Contracts
{
    public interface IComposite : IComponent
    {
        ICollection<IComponent> GetComponents();
        IComponent GetComponent(string name);
    }
}
