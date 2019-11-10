using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.ServiceCAD.Interface
{
    public interface IEntityTypeFactory
    {
        IDBText GetDBTextEntity();
        IPalette GetPalette(string name);
        IPalette GetPalette(string name, Guid toolID);
        IPalette GetPalette(string name, string cmd, Guid toolID);
        dynamic GetDatabase();
    }
}
