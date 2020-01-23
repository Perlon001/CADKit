using Autofac;
using CADKit;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;

namespace CADKitElevationMarks.Services
{
    public class IconServiceFactory
    {
        public IIconService GetIconService(DrawingStandards _standard)
        {
            using(var scope = DI.Container.BeginLifetimeScope())
            {
                switch (_standard)
                {
                    case DrawingStandards.PNB01025:
                        return scope.Resolve<IIconServicePNB01025>();
                    case DrawingStandards.Std01:
                        return scope.Resolve<IIconServiceStd01>();
                    default:
                        return scope.Resolve<IIconServiceDefault>();
                }
            }


        }
    }
}
