using System;
using System.Windows.Forms;

namespace CADKitBasic.Views.WF
{
    public partial class _BaseViewWF : UserControl
    {
        public _BaseViewWF()
        {
            InitializeComponent();
        }

        public virtual void RegisterHandlers()
        {
        }

        public void ShowInfo(string content, string caption = "Informacja")
        {
            MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string content, string caption = "Błąd")
        {
            MessageBox.Show(content, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowException(Exception ex, string caption = "Wyjątek")
        {
            MessageBox.Show(ex.Message, caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        public bool ShowYesNoQuestion(string content, string caption = "Pytanie")
        {
            bool result = false;
            DialogResult dialogResult = MessageBox.Show(content, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                result = true;
            }

            return result;
        }
    }
}
