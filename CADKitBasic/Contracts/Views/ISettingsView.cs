using CADKit.Contracts;
using CADKitBasic.Contracts.Presenters;
using CADKitBasic.Views.DTO;
using System.Collections.Generic;

namespace CADKitBasic.Contracts
{
    public interface ISettingsView : IView
    {
        ISettingsPresenter Presenter { get; set; }
        ScaleDTO SelectedScale { get; set; }
        Units SelectedDrawingUnit { get; set; }
        Units SelectedDimensionUnit { get; set; }

        void BindingDrawingUnits(IList<KeyValuePair<string, Units>> units);
        void BindingDimensionUnits(IList<KeyValuePair<string, Units>> units);
        void BindingScale(IList<ScaleDTO> scales);
        void BindingComponents(string groupName, ICollection<IComponent> components);
    }
}
