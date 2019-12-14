using System;

using CADKitElevationMarks.Contracts;

namespace CADKitElevationMarks.Models
{
    
    public class ElevationMarkFactoryPNB01025 : ElevationMarkFactory
    {
        public override IElevationMark ArchitecturalElevationMark()
        {
            return new ArchitecturalElevationMarkPNB01025();
        }

        public override IElevationMark ConstructionElevationMark()
        {
            return new ConstructionElevationMarkPNB01025();
        }

        public override IElevationMark PlaneElevationMark()
        {
            return new PlaneElevationMarkPNB01025();
        }
    }
}
