using ZwSoft.ZwCAD.Colors;

namespace CADKit.Contract
{
    public interface IEntity
    {
        string Layer { get; set; }
        Color Color { get; set; }
    }
}
