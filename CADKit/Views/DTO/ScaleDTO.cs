using CADKitCore.Contract.DTO;
using System;

namespace CADKitCore.Views.DTO
{
    public class ScaleDTO : IScaleDTO
    {
        public IntPtr UniqueIdentifier { get; set; }
        public string Name { get; set; }
    }
}
