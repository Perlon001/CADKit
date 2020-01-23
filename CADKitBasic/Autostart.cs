using Autofac;
using CADKitBasic;
using CADKitBasic.Contracts;
using CADKit;
using System.Windows.Forms;
using CADKit.Contracts;
using CADKit.Proxy;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
#endif

namespace CADKitElevationMarks
{
    public class Autostart : IAutostart
    {
        public void Initialize()
        {
            AppSettings.Instance.CADKitPalette.Add("Ustawienia", DI.Container.Resolve<ISettingsView>() as Control);
            AppSettings.Instance.CADKitPalette.Visible = true;

            CADProxy.DocumentCreated -= OnDocumentCreated;
            CADProxy.DocumentCreated += OnDocumentCreated;
            CADProxy.DocumentDestroyed -= OnDocumentDestroyed;
            CADProxy.DocumentDestroyed += OnDocumentDestroyed;

        }

        void OnDocumentCreated(object sender, DocumentCollectionEventArgs e)
        {
            AppSettings.Instance.GetSettingsFromDatabase();
            AppSettings.Instance.SetSettingsToDatabase();
            if (CADProxy.DocumentManager.Count == 1 && AppSettings.Instance.CADKitPalette.PaletteState)
            {
                AppSettings.Instance.CADKitPalette.Visible = true;
            }
        }

        void OnDocumentDestroyed(object sender, DocumentDestroyedEventArgs e)
        {
            if (CADProxy.Document == null)
            {
                AppSettings.Instance.Reset();
            }
        }
    }
}
