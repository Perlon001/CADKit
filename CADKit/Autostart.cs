using CADKit.DIContainer;
using CADKit.ServiceCAD;
using ZwSoft.ZwCAD.Runtime;

[assembly: ExtensionApplication(typeof(CADKit.Autostart))]

namespace CADKit
{
    public class Autostart : IExtensionApplication
    {
        private readonly AppSettings settings = AppSettings.Instance;

        public void Initialize()
        {
            CADProxy.WriteMessage("\nStart CADKit");

            // Create DI Container (load all CADKit*.dll modules)
            try
            {
                DI.Container = Container.Builder.Build();
            }
            catch (System.Exception ex)
            {
                CADProxy.WriteMessage("Błąd: \n" + ex.Message);
            }
            CADProxy.WriteMessage("\n");
        }

        public void Terminate()
        {
        }
    }
}
