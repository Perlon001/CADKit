using CADKit;
using System;
using System.Collections.Generic;
using CADKit.Proxy;
using CADKit.Contracts;
using CADKitElevationMarks.Events;

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
    public class JigVerticalMirrorMark : JigMark
    {
        private bool IsVMirror;
        public override string Suffix => (IsVMirror ? "L" : "R");

        public JigVerticalMirrorMark(IEnumerable<Entity> _entityList, Point3d _originPoint, Point3d _basePoint, IEnumerable<IEntityConverter> _converters = null) : base(_entityList, _originPoint, _basePoint, _converters)
        {
            IsVMirror = false;
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
                OnSuffixChanged(new ChangeMarkSuffixEventArgs(Suffix));
            }

            return SamplerStatus.OK;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            try
            {
                currentPoint = new Point3d(currentPoint.X, basePoint.Y, currentPoint.Z);
                transform = Matrix3d.Displacement(basePoint.GetVectorTo(currentPoint));
                var geometry = draw.Geometry;
                if (geometry != null)
                {
                    geometry.PushModelTransform(transform);
                    foreach (var entity in entities)
                    {
                        geometry.Draw(entity);
                    }
                    geometry.PopModelTransform();
                }

                return true;
            }
            catch (Exception ex)
            {
                CADProxy.Editor.WriteMessage(ex.Message);
                return false;
            }
        }

        private void verticalMirroring()
        {
            double textWidth = 0;
            foreach (var e in entities)
            {
                if (e.GetType() == typeof(DBText))
                {
                    var textArea = CADProxy.GetTextArea(e as DBText);
                    textWidth += textArea[1].X - textArea[0].X;
                }
            }
            textWidth += 4;
            foreach (var e in entities)
            {
                if (e.GetType() == typeof(DBText))
                {
                    e.TransformBy(Matrix3d.Displacement(new Vector3d((IsVMirror ? textWidth : -textWidth) * AppSettings.Get.ScaleFactor, 0, 0)));
                }
                else
                {
                    e.TransformBy(Matrix3d.Mirroring(new Line3d(basePoint, new Vector3d(0, 1, 0))));
                }
            }
            IsVMirror = !IsVMirror;
        }

        private bool needVMirror
        {
            get
            {
                return (currentPoint.X < basePoint.X && !IsVMirror) || (currentPoint.X >= basePoint.X && IsVMirror);
            }
        }

    }
}
