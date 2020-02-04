using Autofac;
using CADKit;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using System;

namespace CADKitElevationMarks.Services
{
    public class MarkTypeServiceFactory
    {
        public IMarkTypeService GetMarkTypeService(DrawingStandards _standard)
        {
            using( var scope = DI.Container.BeginLifetimeScope())
            {
                //TODO: Change switch to call dynamic type
                switch (_standard)
                {
                    case DrawingStandards.PNB01025:
                        return scope.Resolve<IMarkTypeServicePNB01025>() ;
                    case DrawingStandards.Std01:
                        return scope.Resolve<IMarkTypeServiceStd01>();
                    //case DrawingStandards.Std02:
                    //    return scope.Resolve<IMarkTypeServiceStd02>();
                    default:
                        throw new NotSupportedException("Nie obsługiwany standard " + _standard.ToString());
                }
            }
        }
    }
}
