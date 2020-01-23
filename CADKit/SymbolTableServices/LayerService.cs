using CADProxy.Contracts;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADProxy.SymbolTableServices
{
    public class LayerService : SymbolTableService<LayerTable>, ILayerService
    {
    }
}
