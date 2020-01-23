using CADKit.Contracts;
using CADKit.Models;
using CADKitBasic.Contracts.DTO;
using CADKitBasic.Contracts.Presenters;
using System.Collections.Generic;

namespace CADKitBasic.Contracts
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
