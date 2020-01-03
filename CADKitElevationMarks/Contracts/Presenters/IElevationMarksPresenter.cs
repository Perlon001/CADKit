using CADKit.Contracts.Presenters;
using CADKit.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Contracts.Presenters
{
    public interface IElevationMarksPresenter : IPresenter
    {
        void CreateMark(int id);
        void ChangeStandardDrawing(DrawingStandards standard);

    }
}
