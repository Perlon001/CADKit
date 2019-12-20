using CADKitElevationMarks.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Services
{
    public class IconServiceFactoryCADKit : IconServiceFactory, IIconServiceCADKitFactory
    {
        public override IIconService CreateService()
        {
            return new IconServiceCADKit();
        }
    }
}
