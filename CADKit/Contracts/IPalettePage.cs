using CADKit.Contracts.Views;

namespace CADKit.Contracts
{
    public interface IPalettePage
    {
        string Title { get; set; }
        IView View { get; set; }
    }
}
