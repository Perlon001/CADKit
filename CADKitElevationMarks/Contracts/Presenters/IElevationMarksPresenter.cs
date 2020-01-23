using CADKit.Contracts;
using CADKitBasic.Contracts.Presenters;
using CADKitBasic.Models;
using CADKitElevationMarks.Events;
using CADKitElevationMarks.Models;

namespace CADKitElevationMarks.Contracts.Presenters
{
    public interface IElevationMarksPresenter : IPresenter
    {
        void ChangeStandardDrawing(DrawingStandards standard);

        void CreateMark(object sender, BeginCreateMarkEventArgs e);

    }
}
