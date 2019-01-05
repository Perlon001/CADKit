using CADKit.Contract.DTO;
using CADKit.Model;
using System.Collections.Generic;

namespace CADKit.Contract
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
        void BindingComposites(ICompositeContainer container);
    }
}
