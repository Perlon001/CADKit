using CADKit.Contracts;
using CADKit.UI.WF;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Events;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CADKitElevationMarks.Views
{

    public partial class ElevationMarksView : BaseViewWF, IElevationMarksView
    {
        public IElevationMarksPresenter Presenter { get; set; }
        public event BeginCreateMarkEventHandler BeginCreateMark;

        public ElevationMarksView()
        {
            InitializeComponent();
            rbxGroup.Checked = true;
            tabStandards.Controls.Clear();
        }

        public override void RegisterHandlers()
        {
            base.RegisterHandlers();

            BeginCreateMark -= Presenter.CreateMark;
            BeginCreateMark += Presenter.CreateMark;
            tabStandards.SelectedIndexChanged -= TabChange;
            tabStandards.SelectedIndexChanged += TabChange;
        }

        public void ClearDrawingStandars()
        {
            tabStandards.SelectedIndexChanged -= TabChange;
            tabStandards.Controls.Clear();
            tabStandards.SelectedIndexChanged += TabChange;
        }

        public void BindDrawingStandard(DrawingStandards _standard, IList<MarkButtonDTO> _listMarks, IInterfaceSchemeService _colorService)
        {
            var tab = new StandardTabPage(_standard, _colorService)
            {
                UseVisualStyleBackColor = true
            };
            tabStandards.TabPages.Add(tab);
            BindMarkButtons(_standard, _listMarks, _colorService);
        }

        public DrawingStandards GetDrawingStandard()
        {
            return (tabStandards.SelectedTab as StandardTabPage).Standard;
        }

        public EntitiesSet GetSetSelection()
        {
            if (rbxGroup.Checked)
                return EntitiesSet.Group;
            if (rbxBlock.Checked)
                return EntitiesSet.Block;
            throw new NotSupportedException();
        }

        public void SetColorScheme(IInterfaceSchemeService _service)
        {
            this.ChangeColorSchema(_service.GetForeColor(), _service.GetBackColor());
            flowLayoutPanel1.ChangeColorSchema(this.ForeColor, this.BackColor);
            gbxOutputFormat.ChangeColorSchema(this.ForeColor, this.BackColor);
            rbxGroup.ChangeColorSchema(this.ForeColor, this.BackColor);
            rbxBlock.ChangeColorSchema(this.ForeColor, this.BackColor);
            btnOptions.ChangeColorSchema(this.BackColor, this.BackColor);
            btnOptions.Image = Presenter.GetOptionIcon();
            tabStandards.ChangeColorSchema(this.ForeColor, this.BackColor);
        }

        private void BindMarkButtons(DrawingStandards _standard, IList<MarkButtonDTO> _listMarks, IInterfaceSchemeService _colorService)
        {
            var tab = tabStandards.TabPages[_standard.ToString()];
            var flp = tab.Controls[_standard.ToString()] as FlowLayoutPanel;
            flp.Controls.Clear();
            foreach (var item in _listMarks)
            {
                var btn = new Button
                {
                    Tag = item.id,
                    Size = new Size(50, 50),
                    Name = "button_" + item.id,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = _colorService.GetBackColor(),
                    ForeColor = _colorService.GetBackColor(),
                    Image = item.picture
                };
                btn.Click += new EventHandler(ButtonClick);
                toolTips.SetToolTip(btn, item.name);
                flp.Controls.Add(btn);
            }
        }

        private void ButtonClick(object _sender, EventArgs _arg)
        {
            BeginCreateMark?.Invoke(_sender, new BeginCreateMarkEventArgs(Convert.ToInt16(((Button)_sender).Tag)));
        }

        private void TabChange(object _sender, EventArgs _arg)
        {
            var page = tabStandards.SelectedTab as StandardTabPage;
            Presenter.ChangeStandardDrawing(page.Standard);
        }

        private class StandardTabPage : TabPage
        {
            public DrawingStandards Standard;
            public StandardTabPage(DrawingStandards _standard, IInterfaceSchemeService _colorService) : base(_standard.ToString())
            {
                this.Standard = _standard;
                this.Name = _standard.ToString();
                var flp = new FlowLayoutPanel
                {
                    Name = _standard.ToString(),
                    Dock = DockStyle.Fill,
                    ForeColor = _colorService.GetBackColor(),
                    BackColor = _colorService.GetBackColor()
                };
                this.Controls.Add(flp);
            }
        }

    }
}
