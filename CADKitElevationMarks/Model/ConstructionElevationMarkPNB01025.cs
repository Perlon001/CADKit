using CADKitElevationMarks.Contract;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitElevationMarks.Model
{
    public class ConstructionElevationMarkPNB01025 : BaseElevationMark
    {
        private Polyline[] plines = new Polyline[2] { new Polyline(), new Polyline() };

        public ConstructionElevationMarkPNB01025(IElevationMarkConfig config) : base(config)
        {
        }

        protected override void Draw()
        {
            throw new System.NotImplementedException();
        }
    }
}
