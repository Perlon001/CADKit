using CADKit.Contracts;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;
using System.Collections.Generic;

namespace CADKitElevationMarks.Contracts.Views
{
    public interface IElevationMarksView : IView
    {
        IElevationMarksPresenter Presenter { get; set; }
        void ClearDrawingStandars();
        void BindDrawingStandard(DrawingStandards standard, IList<MarkButtonDTO> listMarks, IInterfaceSchemeService service);
        DrawingStandards GetDrawingStandard();

        OutputSet GetSetSelection();
    }
}
