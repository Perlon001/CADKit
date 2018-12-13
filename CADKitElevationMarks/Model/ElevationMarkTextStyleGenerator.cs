using CADKitCore.Contract;
using CADKitElevationMarks.Contract;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitElevationMarks.Model
{
    public class ElevationMarkTextStyleGenerator : SymbolGenerator<TextStyleTable>, IElevationMarkTextStyleGenerator
    {
        public override ObjectId Generate<TRecord>()
        {
            TextStyleTableRecord style = new TextStyleTableRecord();
            style.Name = "ck_koty";
            style.FileName = "simplex.shx";
            style.TextSize = 2;
            style.XScale = 0.65;
            style.ObliquingAngle = 0;

            return Generate(style);
        }
    }
}
