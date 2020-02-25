﻿using CADKit.Contracts;
using CADKit.Proxy;
using CADKit.UI.WF;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CADKitElevationMarks.Views
{

    public partial class ElevationMarksView : BaseViewWF,  IElevationMarksView
    {
        public IElevationMarksPresenter Presenter { get; set; }
        public event BeginMarkCreateEventHandler BeginCreateMark;

        public ElevationMarksView()
        {
            InitializeComponent();
            rbxGroup.Checked = true;
        }

        public override void RegisterHandlers()
        {
            base.RegisterHandlers();

            BeginCreateMark -= Presenter.CreateMark;
            BeginCreateMark += Presenter.CreateMark;
        }

        public OutputSet SetType
        {
            get
            {
                if (rbxGroup.Checked)
                    return OutputSet.group;
                if (rbxBlock.Checked)
                    return OutputSet.block;
                throw new Exception();
            }
        }

        public void BindMarkButtons(IEnumerable<MarkButtonDTO> _listMarks)
        {
            flpMarks.Controls.Clear();
            foreach (var item in _listMarks)
            {
                var btn = new Button
                {
                    Tag = item.id,
                    Size = new Size(50, 50),
                    Name = "button_" + item.id,
                    FlatStyle = FlatStyle.Flat,
                    Image = item.picture
                };
                btn.Click += new EventHandler(ButtonClick);
                toolTips.SetToolTip(btn, item.name);
                flpMarks.Controls.Add(btn);
            }
        }

        public void SetColorScheme(IInterfaceSchemeService _service)
        {
            this.ChangeColorSchema(_service.GetForeColor(), _service.GetBackColor());
            flowLayoutPanel1.ChangeColorSchema(this.ForeColor, this.BackColor);
            gbxOutputFormat.ChangeColorSchema(this.ForeColor, this.BackColor);
            rbxGroup.ChangeColorSchema(this.ForeColor, this.BackColor);
            rbxBlock.ChangeColorSchema(this.ForeColor, this.BackColor);
            btnOptions.ChangeColorSchema(this.BackColor, this.BackColor);
            BindCommonIcons();
            Presenter.FillButtons();
        }

        private void ButtonClick(object _sender, EventArgs _arg)
        {
            try
            {
                var a = Convert.ToInt16(((Button)_sender).Tag);
                BeginCreateMark?.Invoke(_sender, new BeginMarkCreateEventArgs(a));
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void BindCommonIcons()
        {
            btnOptions.Image = Presenter.GetOptionIcon();
        }
    }
}
