using CADKit.ServiceCAD.Interface;
#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif
#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.ServiceCAD.Service
{
    public class LayerService : SymbolTableService<LayerTable>, ILayerService
    {
    }
}
