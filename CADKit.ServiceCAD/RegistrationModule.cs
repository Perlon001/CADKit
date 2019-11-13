using Autofac;
using CADProxy.Contracts;
using CADProxy.SymbolTableServices;
using System;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADProxy
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder
                .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<ISymbolTableService<SymbolTable>>()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces();
        }
    }
}
