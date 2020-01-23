using CADKit.Contracts;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.Services
{
    public class LayerService : SymbolTableService<LayerTable>, ILayerService
    {
    }
}
