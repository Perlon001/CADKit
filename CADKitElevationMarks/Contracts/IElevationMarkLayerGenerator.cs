using CADProxy.Contracts;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif
#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKitElevationMarks.Contracts
{
    public interface IElevationMarkLayerGenerator : ISymbolTableService<LayerTable>
    {
    }
}
