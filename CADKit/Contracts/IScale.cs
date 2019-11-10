using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Contracts
{
    public interface IScale
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
