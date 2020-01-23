using CADKit.ServiceCAD.Interface;
using System;

namespace CADKit.ServiceCAD.Proxy.ZwCAD
{
    public class EntityTypeFactory : IEntityTypeFactory
    {
        public IDBText GetDBTextEntity()
        {
            return new DBText();
        }

        public IPalette GetPalette(string name)
        {
            return new Palette(name);
        }

        public IPalette GetPalette(string name, Guid toolID)
        {
            return new Palette(name, toolID);
        }

        public IPalette GetPalette(string name, string cmd, Guid toolID)
        {
            return new Palette(name, cmd, toolID);
        }

        public dynamic GetDatabase()
        {
            return ZwSoft.ZwCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Database;
        }
    }
}
