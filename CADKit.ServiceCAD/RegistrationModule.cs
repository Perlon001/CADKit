using Autofac;
using System;

namespace CADProxy
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            //builder
            //    .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            //    .AssignableTo<ISymbolTableService>()
            //    .InstancePerLifetimeScope()
            //    .AsImplementedInterfaces();
        }
    }
}
