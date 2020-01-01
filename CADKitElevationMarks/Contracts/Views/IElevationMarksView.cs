using CADKit.Contracts.Views;
using CADKit.Models;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.DTO;
using System.Collections.Generic;

namespace CADKitElevationMarks.Contracts.Views
{
    public interface IElevationMarksView : IView
    {
        IElevationMarksPresenter Presenter { get; set; }
        void BindDrawingStandard(DrawingStandards standard, IList<MarkButtonDTO> listMarks);
        DrawingStandards GetDrawingStandard();

    }
}
