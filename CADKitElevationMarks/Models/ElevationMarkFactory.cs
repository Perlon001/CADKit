using Autofac;
using CADKit;
using CADKitElevationMarks.Contracts;

namespace CADKitElevationMarks.Models
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
