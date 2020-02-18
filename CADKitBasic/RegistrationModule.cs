using Autofac;
using CADKitBasic.Contracts.Services;
using System;

namespace CADKitBasic
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<ICompositeService>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<ICompositeProvider>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
        }
    }
}
