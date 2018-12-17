using CADKitCore.Contract;
using CADKitCore.Settings;
using ZwSoft.ZwCAD.Colors;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Model
{
    public class LayerCreator : SymbolRepository<Layers, LayerTableRecord>, ILayerCreator
    {
        protected override void FillDictionary()
        {
            LayerTableRecord layer;

            layer = new LayerTableRecord();
            layer.Name = "kotyWysokosciowe";
            layer.Color = Color.FromColorIndex(ColorMethod.ByAci, 1);
            AddSymbol(Layers.elevmark, layer);
        }
    }
}
