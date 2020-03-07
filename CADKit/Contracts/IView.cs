using System;

namespace CADKit.Contracts
{
    public interface IView : IDisposable
    {
        event EventHandler Load;
        event EventHandler Disposed;

        //void ShowInfo(string content, string caption = "");
        //void ShowError(string content, string caption = "");
        //void ShowException(Exception ex, string caption = "");
        //bool ShowYesNoQuestion(string content, string caption = "");
        void RegisterHandlers();
        void SetColorScheme(IInterfaceSchemeService service);
    }
}
