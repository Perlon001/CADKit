using CADKit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Contract.Services
{
    public interface ICompositeProvider
    {
        SortedSet<Composite> GetModules();
    }
}
