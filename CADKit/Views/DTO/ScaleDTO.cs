using CADKit.Contracts.DTO;
using CADProxy;
using System;

namespace CADKit.Views.DTO
{
    public class ScaleDTO : IScaleDTO
    {
        public IntPtr UniqueIdentifier { get; set; }
        public string Name { get; set; }

        public static ScaleDTO GetCurrentScale()
        {
            return new ScaleDTO()
            {
                Name = CADProxy.ProxyCAD.Database.Cannoscale.Name,
                UniqueIdentifier = CADProxy.ProxyCAD.Database.Cannoscale.UniqueIdentifier
            };
        }
    }
}
