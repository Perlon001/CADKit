#if ZwCAD
using ZwSoft.ZwCAD.Colors;
#endif
#if AutoCAD
using Autodesk.AutoCAD.Colors;
#endif

namespace CADKit.Contracts.Entities
{
    public interface IEntity
    {
        Color Color { get; set; }
        string Layer { get; set; }
    }
}
