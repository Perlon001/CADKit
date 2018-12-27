using Autofac;
using CADKit.Contract;
using System;

namespace CADKit.Views.WF
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
