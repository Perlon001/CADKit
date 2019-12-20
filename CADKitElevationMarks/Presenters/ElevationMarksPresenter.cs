using CADKit.Presenters;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.Models;
using CADKitElevationMarks.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Presenters
{
    public class ElevationMarksPresenter : Presenter<IElevationMarksView>, IElevationMarksPresenter
    {
        public ElevationMarksPresenter(IElevationMarksView view)
        {
            View = view;
            View.Presenter = this;
        }

        public void CreateMark(string kota)
        {
            CADProxy.ProxyCAD.Editor.WriteMessage(kota);
        }

        public override void OnViewLoaded()
        {
            base.OnViewLoaded();
            var service = new MarkTypeService();
            View.BindMarkTypesList(service.GetStandards());
            View.AddMarkButtons(service.GetMarksList());
        }
    }
}
