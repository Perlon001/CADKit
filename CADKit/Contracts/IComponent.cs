using System.Drawing;

namespace CADKit.Contracts
{
    public interface IComponent
    {
        string Name { get; }
        string Title { get; set; }
        Image Image { get; set; }
        object Tag { get; set; }
        IComposite Parent { get; set; }
        bool IsComposite { get; }
    }
}
