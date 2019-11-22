using Autofac;
using CADKit;
using CADKitElevationMarks.Contracts;

namespace CADKitElevationMarks.Models
{
    public abstract class ElevationMarkFactory : IElevationMarkFactory
    {
        public IElevationMark Create(ElevationMarkType type)
        {
            IElevationMarkConfig config;
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                config = scope.Resolve<IElevationMarkConfig>();
            }
            return Create(type, config);
        }

        public abstract IElevationMark Create(ElevationMarkType type, IElevationMarkConfig config);
    }
}
