using CADKitElevationMarks.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Services
{
    public class IconServiceFactoryPNB01025 : IconServiceFactory, IIconServicePNB01025Factory
    {
        public override IIconService CreateService()
        {
            return new IconServicePNB01025();

        }
    }
}
