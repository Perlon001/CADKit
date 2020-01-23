using CADKit.Contracts;

namespace CADKit.Contracts
{
    public interface IPalettePage
    {
        string Title { get; set; }
        IView View { get; set; }
    }
}
