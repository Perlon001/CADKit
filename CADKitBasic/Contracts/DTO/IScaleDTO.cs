using System;

namespace CADKit.Contracts.DTO
{
    public interface IScaleDTO
    {
        IntPtr UniqueIdentifier { get; }
        string Name { get; set; }
    }
}
