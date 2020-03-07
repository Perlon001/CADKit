using CADKit.Contracts;
using CADKitElevationMarks.Events;
using CADKitElevationMarks.Models;
using System.Collections.Generic;
using System.Drawing;

namespace CADKitElevationMarks.Contracts.Presenters
{
    public interface IElevationMarksPresenter : IPresenter
    {
        void CreateMark(object sender, BeginMarkCreateEventArgs e);
        Bitmap GetOptionIcon();
        void FillButtons();
        void FillProperties();

        void FillComponents(int id);
    }
}
