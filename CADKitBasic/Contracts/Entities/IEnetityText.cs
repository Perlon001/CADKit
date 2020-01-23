#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif
#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.Contracts.Entities
{
    public interface IEntityText : IEntity
    {
        TextStyleTableRecord TextStyle { get; set; }
    }
}
