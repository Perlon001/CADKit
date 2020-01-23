using Autofac;

namespace CADKitBasic
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
