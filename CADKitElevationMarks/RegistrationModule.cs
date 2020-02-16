using Autofac;
using CADKitElevationMarks.Contract.Services;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using CADKitElevationMarks.Services;
using System;

namespace CADKitElevationMarks
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<MarkIconDrawingStandardService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<IMarkIconService>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<IMarkService>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<ValueProvider>()
                .InstancePerLifetimeScope()
                .AsSelf();

            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<Mark>()
                .InstancePerLifetimeScope()
                .AsSelf();
        }
    }
}
