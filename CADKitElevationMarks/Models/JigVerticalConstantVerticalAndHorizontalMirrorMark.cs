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
        private bool isVMirror;
        private bool isHMirror;
        public JigVerticalConstantVerticalAndHorizontalMirrorMark(IEnumerable<Entity> _entityList, Point3d _basePoint) : base(_entityList, _basePoint)
        {
            isVMirror = false;
            isHMirror = false;
        }

        public override string GetSuffix()
        {
            return (isVMirror ? "L" : "R") + (isHMirror ? "B" : "T");
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            var result = base.Sampler(prompts);
            if (result != SamplerStatus.OK)
            {
                return result;
            }
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
                currentPoint = new Point3d(currentPoint.X, basePoint.Y, currentPoint.Z);
                transforms = Matrix3d.Displacement(basePoint.GetVectorTo(currentPoint));
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
                    var textArea = ProxyCAD.GetTextArea(e as DBText);
                    textWidth += textArea[1].X - textArea[0].X;
                }
            }
            foreach (var e in entityList)
            {
                if (e.GetType() == typeof(DBText))
                {
                    e.TransformBy(Matrix3d.Displacement(new Vector3d((isVMirror ? textWidth : -textWidth) * AppSettings.Instance.ScaleFactor, 0, 0)));
                }
                else
                {
                    e.TransformBy(Matrix3d.Mirroring(new Line3d(basePoint, new Vector3d(0, 1, 0))));
                }
            }
            isVMirror = !isVMirror;
        }

        private void horizontalMirroring()
        {
            foreach (var e in entityList)
            {
                if (e.GetType() == typeof(DBText))
                {
                    e.TransformBy(Matrix3d.Displacement(new Vector3d(0, (isHMirror ? 9 : -9) * AppSettings.Instance.ScaleFactor, 0)));
                }
                else
                {
                    e.TransformBy(Matrix3d.Mirroring(new Line3d(basePoint, new Vector3d(1, 0, 0))));
                }
            }
            isHMirror = !isHMirror;
        }

        private bool needVMirror
        {
            get
            {
                return (currentPoint.X < basePoint.X && !isVMirror) || (currentPoint.X >= basePoint.X && isVMirror);
            }
        }

        private bool needHMirror
        {
            get
            {
                return (currentPoint.Y < basePoint.Y && !isHMirror) || (currentPoint.Y >= basePoint.Y && isHMirror);
            }
        }
    }
}
