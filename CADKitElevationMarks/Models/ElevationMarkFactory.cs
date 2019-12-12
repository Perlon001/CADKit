
using Autofac;
using CADKit;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Modelsm;

namespace CADKitElevationMarks.Models
{
    public abstract class ElevationMarkFactory
    {
        public abstract IElevationMark ArchitecturalElevationMark();
        public abstract IElevationMark ConstructionElevationMark();
        public abstract IElevationMark PlaneElevationMark();
    }
}
