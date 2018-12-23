using CADKitElevationMarks.Contract;
using System;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitElevationMarks.Model
{
    public class PlaneElevationMarkCADKit : BaseElevationMark
    {
        public PlaneElevationMarkCADKit(IElevationMarkConfig config) : base(config)
        {
        }

        protected override void Draw(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
