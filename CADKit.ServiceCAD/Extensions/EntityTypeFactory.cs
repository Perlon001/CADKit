using CADKit.ServiceCAD.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.ServiceCAD.Proxy.AutoCAD
{
    public class EntityTypeFactory : IEntityTypeFactory
    {
        public dynamic GetDatabase()
        {
            throw new NotImplementedException();
        }

        public IDBText GetDBTextEntity()
        {
            return new DBText();
        }

        public IPalette GetPalette(string name)
        {
            throw new NotImplementedException();
        }

        public IPalette GetPalette(string name, Guid toolID)
        {
            throw new NotImplementedException();
        }

        public IPalette GetPalette(string name, string cmd, Guid toolID)
        {
            throw new NotImplementedException();
        }
    }
}
