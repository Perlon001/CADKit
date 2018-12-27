using CADKitCore.Contract.DALCAD;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Model
{
    public class TextStyleTableService : SymbolTableService<TextStyleTable>, ITextStyleTableService
    {
        public TextStyleTableService(ITextStyleRepository _creator) : base(_creator)
        {
        }
    }
}
