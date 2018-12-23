using CADKitElevationMarks.Contract;
using System;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitElevationMarks.Model
{
    public class PlaneElevationMarkPNB01025 : BaseElevationMark
    {
        public PlaneElevationMarkPNB01025(IElevationMarkConfig config) : base(config)
        {
        }

        protected override void Draw(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
