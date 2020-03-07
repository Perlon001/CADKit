using CADKit.UI.WF;
using CADKit.Contracts;
using CADKitElevationMarks.Contracts.Presenters;
using CADKitElevationMarks.Contracts.Views;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CADKitElevationMarks.Models;
using System.Linq;

namespace CADKitElevationMarks.Views
{

    public partial class ElevationMarksView : UserControl,  IElevationMarksView
    {
        public IElevationMarksPresenter Presenter { get; set; }
        public event BeginMarkCreateEventHandler BeginCreateMark;

        public ElevationMarksView() : base()
        {
            InitializeComponent();
            rbxGroup.Checked = true;
        }

        public void RegisterHandlers()
        {
            BeginCreateMark -= Presenter.CreateMark;
            BeginCreateMark += Presenter.CreateMark;
            cmbMarkType.SelectedIndexChanged -= OnChangeComboMark;
            cmbMarkType.SelectedIndexChanged += OnChangeComboMark;
            cmbMarkType.SelectedIndex = 0;
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

        private class MarkComboItem
        {
            public string Name { get; set; }
            public int Value { get; set; }
            public override string ToString()
            {
                return Name;
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
                cmbMarkType.Items.Add(new MarkComboItem() { Name = item.name, Value = item.id });
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
            pnlProperties.ChangeColorSchema(this.BackColor, this.BackColor);
            cmbMarkType.ChangeColorSchema(this.ForeColor, this.BackColor);
            treeComponents.ChangeColorSchema(this.ForeColor, this.BackColor);
            BindCommonIcons();
            Presenter.FillButtons();
        }

        private void ButtonClick(object _sender, EventArgs _arg)
        {
            var a = Convert.ToInt16(((Button)_sender).Tag);
            BeginCreateMark?.Invoke(_sender, new BeginMarkCreateEventArgs(a));
        }

        private void OnChangeComboMark(object _sender, EventArgs _arg)
        {
            Presenter.FillComponents(cmbMarkType.SelectedIndex);
        }

        private void BindCommonIcons()
        {
            btnOptions.Image = Presenter.GetOptionIcon();
        }

        private void ViewResize(object sender, EventArgs e)
        {
            int max = flpMarks.Controls[flpMarks.Controls.Count - 1].Location.Y;
            pnlProperties.Size = new Size(this.Size.Width, this.Size.Height-max-100);
            cmbMarkType.Width = this.Width - 5;
        }

        public void AddMarkProperty(string markName, IEnumerable<MarkComponentPropertyDTO> component)
        {
            throw new NotImplementedException();
        }

        public void BindMarkComponents(IEnumerable<MarkComponentPropertyDTO> components)
        {
            throw new NotImplementedException();
        }

        public void BindComponents(ICollection<IComponent> components)
        {
            treeComponents.Nodes.Clear();
            treeComponents.Nodes.Add(new TreeNode("Rzędna wysokościowa", AddNode(components)));
            treeComponents.ExpandAll();
        }

        private TreeNode[] AddNode(ICollection<IComponent> composite)
        {
            var com = composite.ToList();
            TreeNode[] nodes = new TreeNode[com.Count];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = com[i].IsComposite
                    ? new TreeNode(com[i].Title ?? com[i].Name, AddNode((com[i] as IComposite).GetComponents()))
                    : new TreeNode(com[i].Title ?? com[i].Name);
            }

            return nodes;
        }
    }
}
