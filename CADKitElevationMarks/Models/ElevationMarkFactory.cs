using Autofac;
using CADKit;
using CADKitElevationMarks.Contracts;

namespace CADKitElevationMarks.Models
{
    public abstract class ElevationMarkFactory : IElevationMarkFactory
    {
        public IElevationMark ElevationMark(ElevationMarkType type)
        {
            IElevationMarkConfig config;
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                config = scope.Resolve<IElevationMarkConfig>();
            }
            return ElevationMark(type, config);
        }

        public abstract IElevationMark ElevationMark(ElevationMarkType type, IElevationMarkConfig config);
    }
}
