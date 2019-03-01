using CADKit.Contract;
using CADKit.Contract.DTO;
using CADKit.Model;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace CADKit.Views.WF
{
    public partial class SettingsView : BaseView.WF.BaseViewWF, ISettingsView
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

        public IScaleDTO SelectedScale
        {
            get { return (IScaleDTO)cmbScale.SelectedItem; }
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

        public void BindingScale(IList<IScaleDTO> scales)
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

        public void BindingComposites(ICollection<Composite> component)
        {
            foreach(var item in component)
            {
                trvComposites.Nodes.Add(new TreeNode(item.LeafTitle,AddNode(item.GetLeafs())));
                trvComposites.Nodes[trvComposites.Nodes.Count-1].Expand();
            }
        }

        private TreeNode[] AddNode(ICollection<IComponent> composite)
        {
            var com = composite.ToList();
            TreeNode[] nodes = new TreeNode[com.Count];
            for(int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new TreeNode(com[i].LeafTitle,AddNode(((Composite)com[i]).GetLeafs()));
            }

            return nodes;
        }

        public override void RegisterHandlers()
        {
            cmbDrawUnit.SelectedIndexChanged -= Presenter.OnDrawUnitSelect;
            cmbDrawUnit.SelectedIndexChanged += Presenter.OnDrawUnitSelect;
            cmbDimUnit.SelectedIndexChanged -= Presenter.OnDimUnitSelect;
            cmbDimUnit.SelectedIndexChanged += Presenter.OnDimUnitSelect;
            cmbScale.SelectedIndexChanged -= Presenter.OnScaleSelect;
            cmbScale.SelectedIndexChanged += Presenter.OnScaleSelect;
        }
    }
}
