using CADKitElevationMarks.Contract;
using System;

namespace CADKitElevationMarks.Model
{
    public class PlaneElevationMarkCADKit : BaseElevationMark
    {
        public PlaneElevationMarkCADKit(IElevationMarkConfig config) : base(config)
        {
        }

        protected override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
