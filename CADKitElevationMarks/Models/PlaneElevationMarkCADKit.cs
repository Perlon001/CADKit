using CADKitElevationMarks.Contracts;
using System;
using System.Collections.Generic;
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
        public PlaneElevationMarkCADKit(ElevationValue _value, IElevationMarkConfig _config) : base(_value, _config)
        {
        }

        protected override void CreateEntityList()
        {
            throw new System.NotImplementedException();
        }

    }
}
