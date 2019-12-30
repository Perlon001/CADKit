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
                case DrawingStandards.CADKit:
                    return new MarkTypeServiceCADKit();
                default:
                    throw new NotSupportedException("Nie obsługiwany standard " + _standard.ToString());
            }
        }
    }
}
