using CADKit.Proxy;
using CADKitBasic.Contracts.DTO;
using System;

namespace CADKitBasic.Views.DTO
{
    public class ScaleDTO : IScaleDTO
    {
        public IntPtr UniqueIdentifier { get; set; }
        public string Name { get; set; }

        public static ScaleDTO GetCurrentScale()
        {
            return new ScaleDTO()
            {
                Name = CADProxy.Database.Cannoscale.Name,
                UniqueIdentifier = CADProxy.Database.Cannoscale.UniqueIdentifier
            };
        }
    }
}
