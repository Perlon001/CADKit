using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKit.Contract
{
    public interface IEntityText : IEntity
    {
        TextStyleTableRecord TextStyle { get; set; }
    }
}
