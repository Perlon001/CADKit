using CADProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
#endif

namespace CADKitElevationMarks.Models
{
    public class JigVerticalConstantMark : JigDisplacement
    {
        public JigVerticalConstantMark(IEnumerable<Entity> _group, Point3d _basePoint) : base(_group, _basePoint)
        {
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            try
            {
                Transforms = Matrix3d.Displacement(basePoint.GetVectorTo(new Point3d(currentPoint.X, basePoint.Y, currentPoint.Z)));
                var geometry = draw.Geometry;
                if (geometry != null)
                {
                    geometry.PushModelTransform(Transforms);
                    foreach (var entity in entityList)
                    {
                        geometry.Draw(entity);
                    }
                    geometry.PopModelTransform();
                }

                return true;
            }
            catch (Exception ex)
            {
                ProxyCAD.Editor.WriteMessage(ex.Message);
                return false;
            }
        }
    }
}
