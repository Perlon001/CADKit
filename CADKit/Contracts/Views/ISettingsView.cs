using CADKit.Contracts.DTO;
using CADKit.Contracts.Presenters;
using CADKit.Contracts.Views;
using CADKit.Models;
using System.Collections.Generic;

namespace CADKit.Contracts
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
        void BindingComposites(ICollection<Composite> collection);
    }
}
