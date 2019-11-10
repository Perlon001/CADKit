using Autofac;
using CADKit.Contracts;
using CADKit.Contracts.Presenters;
using System;

namespace CADKit.Presenters
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
