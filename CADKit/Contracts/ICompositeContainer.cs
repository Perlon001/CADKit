using CADKit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Contracts
{
    public interface ICompositeContainer
    {
        void Register(string name, Composite composite);
        void Unregister(string name);

        IDictionary<string, Composite> GetLeafs();
    }

}
