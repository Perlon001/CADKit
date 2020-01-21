using CADKit.Contracts.Presenters;
using CADKit.Models;
using CADKitElevationMarks.Events;

namespace CADKitElevationMarks.Contracts.Presenters
{
    public interface IElevationMarksPresenter : IPresenter
    {
        void ChangeStandardDrawing(DrawingStandards standard);

        void CreateMark(object sender, BeginCreateMarkEventArgs e);

    }
}
