using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.ServiceCAD.Proxy
{
    public class SystemVariableChangedEventArgs : EventArgs
    {
        public string Name { get; }
        public bool Changed { get; }
    }
}
