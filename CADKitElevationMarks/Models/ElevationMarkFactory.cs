using CADKitElevationMarks.Contracts;

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


        public abstract IElevationMark ArchitecturalElevationMark(ElevationValue value, IElevationMarkConfig config);

        public abstract IElevationMark ConstructionElevationMark(ElevationValue value, IElevationMarkConfig config);

        public abstract IElevationMark PlaneElevationMark(ElevationValue value, IElevationMarkConfig config);

    }
}
