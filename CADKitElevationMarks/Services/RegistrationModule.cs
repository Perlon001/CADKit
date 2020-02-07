using Autofac;
using CADKitElevationMarks.Contracts.Services;
using System;

namespace CADKitElevationMarks.Services
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<IMarkIconService>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<IMarkTypeService>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();

            builder
                .RegisterType<MarkTypeServiceFactory>()
                .InstancePerLifetimeScope()
                .AsSelf();
        }
    }
}
