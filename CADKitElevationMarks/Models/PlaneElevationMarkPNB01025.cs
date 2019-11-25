using CADKitElevationMarks.Contracts;
using System;
#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
#endif
#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Models
{
    public class PlaneElevationMarkPNB01025 : BaseElevationMark
    {
        public PlaneElevationMarkPNB01025(IElevationMarkConfig _config, DrawJig _jig) : base(_config, _jig)
        {
        }

        protected override void Draw(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
