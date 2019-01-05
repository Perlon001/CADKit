using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Util
{
    public abstract class CompositeModule
    {
        protected virtual void Load(CADKitModuleContainerBuilder builder)
        {
        }
    }
}
