using CADKit.Contracts;
using System;

namespace CADKit.Models
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
