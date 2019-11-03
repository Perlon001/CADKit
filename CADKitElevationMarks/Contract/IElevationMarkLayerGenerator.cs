using CADKit.ServiceCAD.Interface;
#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif
#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKitElevationMarks.Contract
{
    public interface IElevationMarkLayerGenerator : ISymbolTableService<LayerTable>
    {
    }
}
