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
using CADKitElevationMarks.Models;
using CADKit.Models;
using CADKitElevationMarks.DTO;

namespace CADKitElevationMarks.Views
{
    public partial class ElevationMarksView : BaseViewWF, IElevationMarksView
    {
        public IElevationMarksPresenter Presenter { get; set; }

        public ElevationMarksView()
        {
            InitializeComponent();
        }

        public void BindMarkTypesList(Dictionary<string, DrawingStandards> standards)
        {
            int i = 1;

            foreach (var item in standards)
            {
                CheckBox chb = new CheckBox();
                chb.Name = "checkbox_" + i++;
                chb.Text = item.Key+ i;
                flpDrawingStandards.Controls.Add(chb);
            }
        }

        public void AddMarkButtons(IList<MarkButtonDTO> _listMarks)
        {
            int i = 1;
            foreach (var item in _listMarks)
            {
                Button btn = new Button();
                btn.Size = new Size(50, 50);
                btn.Name = "button_" + i++;
                btn.Text = btn.Name;
                btn.Click += new EventHandler(btnClick);
                flpMarksPanel.Controls.Add(btn);
            }
        }

        private void btnClick(object sender, EventArgs e)
        {
            Presenter.CreateMark("Robię kotę "+sender.ToString());
        }
    }
}
