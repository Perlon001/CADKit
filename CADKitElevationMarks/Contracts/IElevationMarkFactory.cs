using CADKitElevationMarks.Models;
using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.GraphicsInterface;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsInterface;
#endif
namespace CADKitElevationMarks.Contracts
{
    public interface IElevationMarkFactory
    {
        //IElevationMark CreateElevationMarkFactory(ElevationMarkType type, ElevationValue value);
        //IElevationMark CreateElevationMarkFactory(ElevationMarkType type, ElevationValue value, IElevationMarkConfig config);

        IElevationMark ArchitecturalElevationMark(ElevationValue value, IElevationMarkConfig config);
        IElevationMark ConstructionElevationMark(ElevationValue value, IElevationMarkConfig config);
        IElevationMark PlaneElevationMark(ElevationValue value, IElevationMarkConfig config);
    }
}