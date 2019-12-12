using Autofac;
using CADKitElevationMarks.Contracts;
using System;

namespace CADKitElevationMarks.Models
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<IElevationMarkFactory>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<IElevationMarkConfig>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();

        }
    }
}
