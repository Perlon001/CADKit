using CADKit.Contracts;
using CADKitElevationMarks.Events;
using CADKitElevationMarks.Models;
using System.Drawing;

namespace CADKitElevationMarks.Contracts.Presenters
{
    public interface IElevationMarksPresenter : IPresenter
    {
        void CreateMark(object sender, BeginCreateMarkEventArgs e);
        Bitmap GetOptionIcon();
        void FillButtons();
    }
}
