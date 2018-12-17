using CADKitCore.Contract;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Model
{
    public class LayerTableService : SymbolTableService<LayerTable>, ILayerTableService
    {
        public LayerTableService(ILayerCreator _creator) : base(_creator)
        {
        }
    }
}
