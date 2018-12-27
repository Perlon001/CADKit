using CADKit.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Model
{
    public class CanNoScale : IScale
    {
        public IntPtr UniqueIdentifier { get; set; }

        public double PaperUnits { get; set; }
        public string Name { get; set; }

        public bool IsTemporaryScale { get; set; }

        public double DrawingUnits { get; set; }

        public string CollectionName { get; set; }

        public double Scale { get; set; }
    }
}
