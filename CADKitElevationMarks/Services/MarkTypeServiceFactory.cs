using CADKit.Models;
using CADKitElevationMarks.Contracts.Services;
using System;

namespace CADKitElevationMarks.Services
{
    public class MarkTypeServiceFactory
    {
        public IMarkTypeService GetMarkTypeService(DrawingStandards _standard)
        {
            switch (_standard)
            {
                case DrawingStandards.PNB01025:
                    return new MarkTypeServicePNB01025();
                case DrawingStandards.Std01:
                    return new MarkTypeServiceStd01();
                case DrawingStandards.Std02:
                    return new MarkTypeServiceStd02();
                default:
                    throw new NotSupportedException("Nie obsługiwany standard " + _standard.ToString());
            }
        }
    }
}
