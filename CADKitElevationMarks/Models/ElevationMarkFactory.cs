
using Autofac;
using CADKit;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Modelsm;

namespace CADKitElevationMarks.Models
{
    public abstract class ElevationMarkFactory : IElevationMarkFactory
    {
        //public IElevationMark CreateElevationMarkFactory(ElevationMarkType _type, ElevationValue _value)
        //{
        //    IElevationMarkConfig config;
        //    using (var scope = DI.Container.BeginLifetimeScope())
        //    {
        //        config = scope.Resolve<IElevationMarkConfig>();
        //    }
        //    return CreateElevationMarkFactory(_type, _value, config);
        //}

        //public abstract IElevationMark CreateElevationMarkFactory(ElevationMarkType type, ElevationValue value, IElevationMarkConfig config);

        public abstract ElevationMark ArchitecturalElevationMark();

        public abstract ElevationMark ConstructionElevationMark();

        public abstract ElevationMark PlaneElevationMark();

    }
}
