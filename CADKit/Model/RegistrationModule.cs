using Autofac;
using CADKitCore.Contract.DALCAD;
using System;

namespace CADKitCore.Model
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<ISymbolRepository>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();

            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<ISymbolTableService>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
        }
    }
}
