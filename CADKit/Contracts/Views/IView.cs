using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Contracts.Views
{
    public interface IView : IDisposable
    {
        event EventHandler Load;
        event EventHandler Disposed;

        void ShowInfo(string content, string caption = "");
        void ShowError(string content, string caption = "");
        void ShowException(Exception ex, string caption = "");
        bool ShowYesNoQuestion(string content, string caption = "");

        void RegisterHandlers();

    }
}
