using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CADKitBasic.Views.WPF
{
    /// <summary>
    /// Interaction logic for BaseViewWPF.xaml
    /// </summary>
    public partial class BaseViewWPF : UserControl
    {
        public BaseViewWPF()
        {
            //InitializeComponent();
        }
        public void ShowInfo(string content, string caption = "Informacja")
        {
            MessageBox.Show(content, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowError(string content, string caption = "Błąd")
        {
            MessageBox.Show(content, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowException(Exception ex, string caption = "Wyjątek")
        {
            MessageBox.Show(ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Question);
        }

        public bool ShowYesNoQuestion(string content, string caption = "Pytanie")
        {
            bool result = false;
            MessageBoxResult dialogResult = MessageBox.Show(content, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (dialogResult == MessageBoxResult.Yes)
            {
                result = true;
            }

            return result;
        }
    }
}
