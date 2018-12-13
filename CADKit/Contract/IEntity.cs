using ZwSoft.ZwCAD.Colors;

namespace CADKitCore.Contract
{
    public interface IEntity
    {
        string Layer { get; set; }
        Color Color { get; set; }
    }
}
