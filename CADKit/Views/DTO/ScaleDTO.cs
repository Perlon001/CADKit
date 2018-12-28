using CADKit.Contract.DTO;
using CADKit.ServiceCAD;
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
                Name = CADProxy.Database.Cannoscale.Name,
                UniqueIdentifier = CADProxy.Database.Cannoscale.UniqueIdentifier
            };
        }
    }
}
