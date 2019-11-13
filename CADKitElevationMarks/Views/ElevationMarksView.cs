using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CADKit.Views.WF;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.Contracts.Presenters;

namespace CADKitElevationMarks.Views
{
    public partial class ElevationMarksView : BaseViewWF, IElevationMarksView
    {
        public IElevationMarksPresenter Presenter { get; set; }

        public ElevationMarksView()
        {
            InitializeComponent();
        }
    }
}
