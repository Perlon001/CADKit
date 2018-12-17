using CADKitCore.Contract;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Model
{
    public class LayerTableService : SymbolTableService<LayerTable>, ILayerTableService
    {
        public LayerTableService(ILayerRepository _creator) : base(_creator)
        {
        }
    }
}
