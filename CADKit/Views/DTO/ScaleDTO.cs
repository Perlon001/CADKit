using CADKit.Contract.DTO;
using System;

namespace CADKit.Views.DTO
{
    public class ScaleDTO : IScaleDTO
    {
        public IntPtr UniqueIdentifier { get; set; }
        public string Name { get; set; }
    }
}
