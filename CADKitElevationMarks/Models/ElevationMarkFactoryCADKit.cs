using System;

using CADKitElevationMarks.Contracts;

namespace CADKitElevationMarks.Models
{
    public class ElevationMarkFactoryCADKit : ElevationMarkFactory
    {
        public override IElevationMark ArchitecturalElevationMark()
        {
            return new ArchitecturalElevationMarkCADKit();
        }

        public override IElevationMark ConstructionElevationMark()
        {
            return new ConstructionElevationMarkCADKit();
        }

        public override IElevationMark PlaneElevationMark()
        {
            return new PlaneElevationMarkCADKit();
        }
    }
}
