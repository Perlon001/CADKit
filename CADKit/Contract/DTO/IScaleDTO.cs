using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitCore.Contract.DTO
{
    public interface IScaleDTO
    {
        IntPtr UniqueIdentifier { get; }
        double Scale { get; }
        double PaperUnits { get; set; }
        string Name { get; set; }
        bool IsTemporaryScale { get; }
        double DrawingUnits { get; set; }
        string CollectionName { get; }
    }
}
