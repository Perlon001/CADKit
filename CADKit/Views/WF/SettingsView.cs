using CADKit.Contract;
using CADKit.Contract.DTO;
using CADKit.Model;
using System.Collections.Generic;
using System.Drawing;

namespace CADKit.Views.WF
{
    public partial class SettingsView : CADKit.BaseView.WF.BaseViewWF, ISettingsView
    {
        public ISettingsPresenter Presenter { get; set; }

        public Units SelectedDrawingUnit
        {
            get { return (Units)cmbDrawUnit.SelectedItem; }
            set { cmbDrawUnit.SelectedValue = value; }
        }

        public Units SelectedDimensionUnit
        {
            get { return (Units)cmbDimUnit.SelectedItem; }
            set { cmbDimUnit.SelectedValue = value; }
        }

        public IScaleDTO SelectedScale
        {
            get { return (IScaleDTO)cmbScale.SelectedItem; }
            set { cmbScale.SelectedValue = value.UniqueIdentifier; }
        }

        public SettingsView()
        {
            InitializeComponent();
        }

        public void BindingDrawingUnits(IList<KeyValuePair<string, Units>> units)
        {
            cmbDrawUnit.SelectedIndexChanged -= Presenter.OnDrawUnitSelect;
            cmbDrawUnit.BackColor = SystemColors.Control;
            cmbDrawUnit.DataSource = units;
            cmbDrawUnit.ValueMember = "Value";
            cmbDrawUnit.DisplayMember = "Key";
            cmbDrawUnit.SelectedIndexChanged += Presenter.OnDrawUnitSelect;
            if (units.Count > 0)
            {
                cmbDrawUnit.BackColor = SystemColors.Window;
            }
        }

        public void BindingDimensionUnits(IList<KeyValuePair<string, Units>> units)
        {
            cmbDimUnit.SelectedIndexChanged -= Presenter.OnDimUnitSelect;
            cmbDimUnit.BackColor = SystemColors.Control;
            cmbDimUnit.DataSource = units;
            cmbDimUnit.ValueMember = "Value";
            cmbDimUnit.DisplayMember = "Key";
            cmbDimUnit.SelectedIndexChanged += Presenter.OnDimUnitSelect;
            if (units.Count > 0)
            {
                cmbDimUnit.BackColor = SystemColors.Window;
            }
        }

        public void BindingScale(IList<IScaleDTO> scales)
        {
            cmbScale.SelectedIndexChanged -= Presenter.OnScaleSelect;
            cmbScale.BackColor = SystemColors.Control;
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
