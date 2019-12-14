using CADKit;
using CADKit.Utils;
using CADProxy;
using System;
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
#endif

namespace CADKitElevationMarks.Models
{
    public class JigVerticalConstantVerticalAndHorizontalMirrorMark : EntityListJig
    {
        private bool IsVMirror;
        private bool IsHMirror;
        public JigVerticalConstantVerticalAndHorizontalMirrorMark(IEnumerable<Entity> _entityList, Point3d _basePoint) : base(_entityList, _basePoint)
        {
            IsVMirror = false;
            IsHMirror = false;
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            var result = base.Sampler(prompts);
            if (result != SamplerStatus.OK)
                return result;
            if (needVMirror)
            {
                verticalMirroring();
            }
            if (needHMirror)
            {
                horizontalMirroring();
            }

            return SamplerStatus.OK;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            try
            {
                transforms = Matrix3d.Displacement(basePoint.GetVectorTo(new Point3d(currentPoint.X, basePoint.Y, currentPoint.Z)));
                var geometry = draw.Geometry;
                if (geometry != null)
                {
                    geometry.PushModelTransform(transforms);
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

        private void verticalMirroring()
        {
            double textWidth = 0;
            foreach(var e in entityList)
            {
                if(e.GetType() == typeof(DBText))
                {
                    var textArea = EntityInfo.GetTextArea(e as DBText);
                    textWidth += textArea[1].X - textArea[0].X;
                }
            }
            textWidth += 1.5;
            foreach (var e in entityList)
            {
                if (e.GetType() == typeof(DBText))
                {
                    e.TransformBy(Matrix3d.Displacement(new Vector3d((IsVMirror ? textWidth : -textWidth) * AppSettings.Instance.ScaleFactor, 0, 0)));
                }
                else
                {
                    e.TransformBy(Matrix3d.Mirroring(new Line3d(basePoint, new Vector3d(0, 1, 0))));
                }
            }
            IsVMirror = !IsVMirror;
        }

        private void horizontalMirroring()
        {
            foreach (var e in entityList)
            {
                if (e.GetType() == typeof(DBText))
                {
                    e.TransformBy(Matrix3d.Displacement(new Vector3d(0, (IsHMirror ? 9 : -9) * AppSettings.Instance.ScaleFactor, 0)));
                }
                else
                {
                    e.TransformBy(Matrix3d.Mirroring(new Line3d(basePoint, new Vector3d(1, 0, 0))));
                }
            }
            IsHMirror = !IsHMirror;
        }

        private bool needVMirror
        {
            get
            {
                return (currentPoint.X < basePoint.X && !IsVMirror) || (currentPoint.X >= basePoint.X && IsVMirror);
            }
        }

        private bool needHMirror
        {
            get
            {
                return (currentPoint.Y < basePoint.Y && !IsHMirror) || (currentPoint.Y >= basePoint.Y && IsHMirror);
            }
        }

    }
}
