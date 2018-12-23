using Autofac;
using CADKitCore.Contract;
using System;

namespace CADKitCore.Presenters
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<IPresenter>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
        }
    }
}
