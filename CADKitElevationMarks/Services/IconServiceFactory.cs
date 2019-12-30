using CADKit.Models;
using CADKitElevationMarks.Contracts.Services;
using System;

namespace CADKitElevationMarks.Services
{
    public class IconServiceFactory
    {
        public IIconService GetIconService(DrawingStandards _standard)
        {
            switch (_standard)
            {
                case DrawingStandards.PNB01025:
                    return new IconServicePNB01025();
                case DrawingStandards.CADKit:
                    return new IconServiceCADKit();
                default:
                    throw new NotSupportedException("Nie zaimplementowany standard " + _standard.ToString());
            }
        }
    }
}
