#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADProxy.Contracts
{
    public interface ILayerService : ISymbolTableService<LayerTable>
    {
    }
}
