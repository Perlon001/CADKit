﻿using CADKit;
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
    public class JigVerticalMirrorMark : EntityListJig
    {
        private bool IsVMirror;
        public JigVerticalMirrorMark(IEnumerable<Entity> _entityList, Point3d _basePoint) : base(_entityList, _basePoint)
        {
            IsVMirror = false;
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

            return SamplerStatus.OK;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            try
            {
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
            foreach (var e in entityList)
            {
                if (e.GetType() == typeof(DBText))
                {
                    var textArea = EntityInfo.GetTextArea(e as DBText);
                    textWidth += textArea[1].X - textArea[0].X;
                }
            }
            textWidth += 4;
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

        private bool needVMirror
        {
            get
            {
                return (currentPoint.X < basePoint.X && !IsVMirror) || (currentPoint.X >= basePoint.X && IsVMirror);
            }
        }

    }
}