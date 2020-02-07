using Autofac;
using CADKit;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;

namespace CADKitElevationMarks.Services
{
    public class MarkIconServiceFactory
    {
        public IMarkIconService GetIconService(DrawingStandards _standard)
        {
            using(var scope = DI.Container.BeginLifetimeScope())
            {
                switch (_standard)
                {
                    case DrawingStandards.PNB01025:
                        return scope.Resolve<IMarkIconServicePNB01025>();
                    case DrawingStandards.Std01:
                        return scope.Resolve<IMarkIconServiceStd01>();
                    default:
                        return scope.Resolve<IMarkIconServiceDefault>();
                }
            }
        }
    }
}
