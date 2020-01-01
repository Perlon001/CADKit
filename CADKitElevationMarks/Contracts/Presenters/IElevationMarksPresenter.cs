using CADKit.Contracts.Presenters;
using CADKit.Models;
using CADKitElevationMarks.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Contracts.Presenters
{
    public interface IElevationMarksPresenter : IPresenter
    {
        void CreateMark(int kota);
        void ChangeStandardDrawing(DrawingStandards standard);
    }
}
