using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CADKit.Views.WF;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.Contracts.Presenters;
using CADKit.Models;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;

namespace CADKitElevationMarks.Views
{  
    public partial class ElevationMarksView : BaseViewWF, IElevationMarksView
    {
        public IElevationMarksPresenter Presenter { get; set; }

        public ElevationMarksView()
        {
            InitializeComponent();
        }
        class StandardTabPage : TabPage
        {
            public DrawingStandards Standard;
            public StandardTabPage(DrawingStandards _standard) : base(_standard.ToString())
            {
                this.Standard = _standard;
                this.Name = _standard.ToString();
                var flp = new FlowLayoutPanel();
                flp.Name = _standard.ToString();
                flp.Dock = DockStyle.Fill;
                this.Controls.Add(flp);
            }
        }

        class MarkButton : Button
        {
            public int id;
        }

        public void BindDrawingStandard(DrawingStandards _standard, IList<MarkButtonDTO> _listMarks)
        {
            tabStandards.TabPages.Add(new StandardTabPage(_standard));
            BindMarkButtons(_standard, _listMarks);
        }
        
        public void BindMarkButtons(DrawingStandards _standard, IList<MarkButtonDTO> _listMarks)
        {
            var tab = tabStandards.TabPages[_standard.ToString()];
            FlowLayoutPanel flp = tab.Controls[_standard.ToString()] as FlowLayoutPanel;
            flp.Controls.Clear();
            foreach (var item in _listMarks)
            {
                MarkButton btn = new MarkButton();
                btn.id = item.id;
                btn.Size = new Size(50, 50);
                btn.Name = "button_" + item.id;
                btn.Text = item.name;
                btn.Click += new EventHandler(btnClick);
                flp.Controls.Add(btn);
            }
        }

        private void btnClick(object sender, EventArgs e)
        {
            Presenter.CreateMark((sender as MarkButton).id);
        }
    }
}
