using CADKit.Contracts.Presenters;
using CADKit.Models;

namespace CADKitElevationMarks.Contracts.Presenters
{
    public interface IElevationMarksPresenter : IPresenter
    {
        void CreateMark(int id);
        void ChangeStandardDrawing(DrawingStandards standard);

    }
}
