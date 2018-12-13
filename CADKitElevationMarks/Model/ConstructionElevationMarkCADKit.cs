using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CADKitElevationMarks.Contract;

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
