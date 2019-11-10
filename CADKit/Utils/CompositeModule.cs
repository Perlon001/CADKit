using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Utils
{
    public abstract class CompositeModule
    {
        protected virtual void Load(CADKitModuleContainerBuilder builder)
        {
        }
    }
}
