using CADKit.Contracts;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.DTO;
using System.Collections.Generic;

namespace CADKitElevationMarks.Contracts.Views
{
    public interface IElevationMarksView : IView
    {
        IElevationMarksPresenter Presenter { get; set; }
        void BindMarkButtons(IEnumerable<MarkButtonDTO> listMarks);
        OutputSet SetType { get; }
    }
}
