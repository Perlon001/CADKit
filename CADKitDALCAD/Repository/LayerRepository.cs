using System.Collections.Generic;
using CADKitCore.Contract.DALCAD;
using CADKitCore.Settings;
using ZwSoft.ZwCAD.Colors;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitDALCAD.Repository
{
    public class LayerRepository : SymbolRepository<Layers, LayerTableRecord>, ILayerRepository
    {
        public override IList<LayerTableRecord> GetRecords()
        {
            throw new System.NotImplementedException();
        }

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
