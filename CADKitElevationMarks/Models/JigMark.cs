using CADKit.Utils;
using CADProxy.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;

namespace CADKitElevationMarks.Models
{
    public class JigMark : EntityListJig
    {
        public JigMark(IEnumerable<Entity> _entityList, Point3d _basePoint) : base(_entityList, _basePoint)
        {
        }
    }
}
