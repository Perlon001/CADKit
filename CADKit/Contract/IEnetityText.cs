using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Contract
{
    public interface IEntityText : IEntity
    {
        TextStyleTableRecord TextStyle { get; set; }
    }
}
