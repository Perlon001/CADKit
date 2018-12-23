using CADKitCore.Contract;
using CADKitCore.Contract.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CADKitCore.Views.WF
{
    public partial class SettingsView : CADKitBaseView.WF.BaseViewWF, ISettingsView
    {
        public ISettingsPresenter Presenter { get; set; }

        public IScaleDTO SelectedScale
        {
            get
            {
                var item = (IScaleDTO)cmbScale.SelectedItem;
                return item;
            }
            set
            {
                cmbScale.SelectedValue = value.UniqueIdentifier;
            }
        }

        public SettingsView()
        {
            InitializeComponent();
        }

        public void BindingScale(IList<IScaleDTO> scales)
        {
            cmbScale.SelectedIndexChanged -= Presenter.OnScaleSelect;
            cmbScale.BackColor = SystemColors.Control;
            cmbScale.DataSource = null;
            cmbScale.DataSource = scales;
            cmbScale.ValueMember = "UniqueIdentifier";
            cmbScale.DisplayMember = "Name";
            cmbScale.SelectedIndexChanged += Presenter.OnScaleSelect;
            if(scales.Count > 0 )
            {
                cmbScale.BackColor = SystemColors.Window;
            }
        }

        public override void RegisterHandlers()
        {
            cmbScale.SelectedIndexChanged -= Presenter.OnScaleSelect;
            cmbScale.SelectedIndexChanged += Presenter.OnScaleSelect;
        }
    }
}
