using System;

namespace CADKitBasic.Contracts.DTO
{
    public interface IScaleDTO
    {
        IntPtr UniqueIdentifier { get; }
        string Name { get; set; }
    }
}
