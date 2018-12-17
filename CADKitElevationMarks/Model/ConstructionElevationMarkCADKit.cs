using CADKitElevationMarks.Contract;
using System;

namespace CADKitElevationMarks.Model
{
    public class ConstructionElevationMarkCADKit : BaseElevationMark
    {
        public ConstructionElevationMarkCADKit(IElevationMarkConfig config) : base(config)
        {
        }

        protected override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
