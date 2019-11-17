using Autofac;

namespace CADKit
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder
            //    .RegisterType<AppSettings>()
            //    .SingleInstance()
            //    .AsSelf();
        }
    }
}
