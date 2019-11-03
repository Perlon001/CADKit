using CADKitElevationMarks.Contract;
using System;
#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif
#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

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
