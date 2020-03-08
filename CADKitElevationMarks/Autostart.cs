using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKitBasic.Contracts;
using CADKitElevationMarks.Contract.Services;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;
using CADKitElevationMarks.Services;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CADKitElevationMarks
{
    public class Autostart : IAutostart
    {
        public void Initialize()
        {
            AppSettings.Get.CADKitPalette.Add("Koty wysokościowe", DI.Container.Resolve<IElevationMarksView>() as Control);
            AppSettings.Get.CADKitPalette.Visible = true;
            using (var scope = DI.Container.BeginLifetimeScope())
            {
                ICollection<IComponent> collection = new List<IComponent>();
                var markService = scope.Resolve<IMarkService>();
                var marks = markService.GetMarks();
                var controlPage = AppSettings.Get.CADKitPalette.GetPage("Ustawienia") as ISettingsView;
                foreach(MarkDTO m in marks)
                {
                    var mark = scope.Resolve(m.markType) as Mark;
                    collection.Add(mark);
                }
                controlPage.BindingComponents("Marki wysokościowe", collection);
            }
        }
    }
}
