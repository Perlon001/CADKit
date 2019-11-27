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
    public class PlaneElevationMarkCADKit : BaseElevationMark
    {
        public PlaneElevationMarkCADKit(IElevationMarkConfig _config) : base(_config)
        {
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        protected override Group DrawEntities(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}
