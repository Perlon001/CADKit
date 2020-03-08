﻿using CADKit.Contracts;
using CADKit.Models;
using CADKit.UI.WF;
using CADKitBasic.Contracts;
using CADKitBasic.Contracts.Presenters;
using CADKitBasic.Views.DTO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CADKitBasic.Views.WF
{
    public partial class SettingsView : UserControl, ISettingsView
    {
        public ISettingsPresenter Presenter { get; set; }

        public Units SelectedDrawingUnit
        {
            get { return ((KeyValuePair<string, Units>)cmbDrawUnit.SelectedItem).Value; }
            set { cmbDrawUnit.SelectedValue = value; }
        }

        public Units SelectedDimensionUnit
        {
            get { return ((KeyValuePair<string, Units>)cmbDimUnit.SelectedItem).Value; }
            set { cmbDimUnit.SelectedValue = value; }
        }

        public ScaleDTO SelectedScale
        {
            get { return (ScaleDTO)cmbScale.SelectedItem; }
            set { cmbScale.SelectedValue = value.Name; }
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

        public void BindingScale(IList<ScaleDTO> scales)
        {
            cmbScale.SelectedIndexChanged -= Presenter.OnScaleSelect;
            cmbScale.BackColor = SystemColors.Control;
            cmbScale.DataSource = scales;
            cmbScale.ValueMember = "Name";
            cmbScale.DisplayMember = "Name";
            cmbScale.SelectedIndexChanged += Presenter.OnScaleSelect;
            if(scales.Count > 0 )
            {
                cmbScale.BackColor = SystemColors.Window;
            }
        }

        public void BindingComponents(string _groupName, ICollection<IComponent> _components)
        {
            trvComposites.Nodes.Add(new TreeNode(_groupName, AddNode(_components)));
            trvComposites.ExpandAll();
        }

        private TreeNode[] AddNode(ICollection<IComponent> composite)
        {
            var com = composite.ToList();
            TreeNode[] nodes = new TreeNode[com.Count];
            for(int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = com[i].IsComposite 
                    ? new TreeNode(com[i].Title, AddNode((com[i] as IComposite).GetComponents())) 
                    : new TreeNode(com[i].Title);
            }

            return nodes;
        }

        public void RegisterHandlers()
        {
            cmbDrawUnit.SelectedIndexChanged -= Presenter.OnDrawUnitSelect;
            cmbDrawUnit.SelectedIndexChanged += Presenter.OnDrawUnitSelect;
            cmbDimUnit.SelectedIndexChanged -= Presenter.OnDimUnitSelect;
            cmbDimUnit.SelectedIndexChanged += Presenter.OnDimUnitSelect;
            cmbScale.SelectedIndexChanged -= Presenter.OnScaleSelect;
            cmbScale.SelectedIndexChanged += Presenter.OnScaleSelect;
        }

        public void SetColorScheme(IInterfaceSchemeService service)
        {
            this.ChangeColorSchema(service.GetForeColor(), service.GetBackColor());
            gbxScale.ChangeColorSchema(this.ForeColor, this.BackColor);
            lblDimUnit.ChangeColorSchema(this.ForeColor, this.BackColor);
            lblDimUnit.ChangeColorSchema(this.ForeColor, this.BackColor);
            lblScale.ChangeColorSchema(this.ForeColor, this.BackColor);
            cmbDimUnit.ChangeColorSchema(this.ForeColor, this.BackColor);
            cmbDrawUnit.ChangeColorSchema(this.ForeColor, this.BackColor);
            cmbScale.ChangeColorSchema(ForeColor, this.BackColor);
        }
    }
}
