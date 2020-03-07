using CADKit.Contracts;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;
using System.Collections.Generic;

namespace CADKitElevationMarks.Contracts.Views
{
    public interface IElevationMarksView : IView
    {
        IElevationMarksPresenter Presenter { get; set; }
        void BindMarkButtons(IEnumerable<MarkButtonDTO> listMarks);
        void BindMarkComponents(IEnumerable<MarkComponentPropertyDTO> components);
        void BindComponents(ICollection<IComponent> components);
        OutputSet SetType { get; }
    }
}
