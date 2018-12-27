using CADKitCore.Contract.DALCAD;
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
