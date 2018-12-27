using CADKitCore.Contract.DTO;
using CADKitCore.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitCore.Contract
{
    public interface ISettingsView : IView
    {
        ISettingsPresenter Presenter { get; set; }
        IScaleDTO SelectedScale { get; set; }
        Units SelectedDrawingUnit { get; set; }
        Units SelectedDimensionUnit { get; set; }

        void BindingDrawingUnits(IList<KeyValuePair<string, Units>> units);
        void BindingDimensionUnits(IList<KeyValuePair<string, Units>> units);
        void BindingScale(IList<IScaleDTO> scales);

    }
}
