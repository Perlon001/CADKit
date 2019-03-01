using Autofac;
using CADKit.Contract;
using CADKit.Contract.Services;
using System;

namespace CADKit.Services
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
