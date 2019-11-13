using Autofac;
using CADKit.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder
            //    .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
            //    .AssignableTo<IAutostart>()
            //    .InstancePerLifetimeScope()
            //    .AsImplementedInterfaces();
        }
    }
}
