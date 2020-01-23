using Autofac;
using CADKit.Contracts.Views;
using System;

namespace CADKit.Views
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<IView>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .AsImplementedInterfaces();
        }
    }
}
