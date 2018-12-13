using CADKitCore.Contract;
using CADKitElevationMarks.Contract;
using ZwSoft.ZwCAD.Colors;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitElevationMarks.Model
{
    public class ElevationMarkLayerGenerator : SymbolGenerator<LayerTable>, IElevationMarkLayerGenerator
    {
        public override ObjectId Generate<TRecord>()
        {
            LayerTableRecord layer = new LayerTableRecord();
            layer.Name = "Wymiary";
            layer.Color = Color.FromColorIndex(ColorMethod.ByAci,1);
            layer.IsPlottable = true;

            return Generate(layer);
        }
    }
}
