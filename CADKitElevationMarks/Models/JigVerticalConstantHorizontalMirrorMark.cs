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
    public class JigVerticalConstantHorizontalMirrorMark : JigDisplacement
    {
        private bool IsMirror;
        public JigVerticalConstantHorizontalMirrorMark(IEnumerable<Entity> _entityList, Point3d _basePoint) : base(_entityList, _basePoint)
        {
            IsMirror = false;
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            var result = base.Sampler(prompts);
            if ( result != SamplerStatus.OK) 
                return result;
            if (needHorizontalMirror)
            {
                horizontalMirroring();
            }

            return SamplerStatus.OK;
        }

        private void horizontalMirroring()
        {
            foreach (var e in entityList)
            {
                if(e.GetType() == typeof(DBText))
                {
                    e.TransformBy(Matrix3d.Displacement(new Vector3d(0, (IsMirror ? 9 : -9), 0)));
                } 
                else e.TransformBy(Matrix3d.Mirroring(new Line3d(basePoint, new Vector3d(1, 0, 0))));
            }
            IsMirror = !IsMirror;
        }

        private bool needHorizontalMirror
        {
            get 
            { 
                return (currentPoint.Y < basePoint.Y && !IsMirror) || (currentPoint.Y >= basePoint.Y && IsMirror);
            }
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
