using CADKit.Contracts.Views;
using CADKit.Models;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Contracts.Views
{
    public interface IElevationMarksView : IView
    {
        IElevationMarksPresenter Presenter { get; set; }

        void BindMarkTypesList(Dictionary<string,DrawingStandards> standards);
        void AddMarkButtons(IList<MarkButtonDTO> _listMarks);
    }
}
