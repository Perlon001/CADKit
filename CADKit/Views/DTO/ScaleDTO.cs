using CADKitCore.Contract.DTO;
using System;

namespace CADKitCore.Views.DTO
{
    public class ScaleDTO : IScaleDTO
    {
        public IntPtr UniqueIdentifier { get; set; }
        public double Scale { get; set; }
        public double PaperUnits { get; set; }
        public string Name { get; set; }
        public bool IsTemporaryScale { get; set; }
        public double DrawingUnits { get; set; }
        public string CollectionName { get; set; }
    }
}
