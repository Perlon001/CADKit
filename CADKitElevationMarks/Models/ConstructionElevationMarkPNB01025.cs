using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Modelsm;
using System;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKitElevationMarks.Models
{
    public class ConstructionElevationMarkPNB01025 : ArchitecturalElevationMarkPNB01025 { }
}
