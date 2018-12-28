using Autofac;
using CADKit.DIContainer;
using CADKitElevationMarks.Contract;

namespace CADKitElevationMarks.Model
{
    public abstract class ElevationMarkFactory : IElevationMarkFactory
    {
        public IElevationMark GetElevationMark(ElevationMarkType type)
        {
            IElevationMarkConfig config;
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                config = scope.Resolve<IElevationMarkConfig>();
            }
            return GetElevationMark(type, config);
        }

        public abstract IElevationMark GetElevationMark(ElevationMarkType type, IElevationMarkConfig config);
    }
}
