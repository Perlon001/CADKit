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
using Autodesk.AutoCAD.GraphicsInterface;
#endif

namespace CADKitElevationMarks.Models
{
    public class JigVerticalConstantVerticalAndHorizontalMirrorMark : JigMark
    {
        private bool isVMirror;
        private bool isHMirror;
        public override string Suffix => (isVMirror ? "L" : "R") + (isHMirror ? "B" : "T");
        public JigVerticalConstantVerticalAndHorizontalMirrorMark(IEnumerable<Entity> _entityList, Point3d _originPoint, Point3d _basePoint, IEnumerable<IEntityConverter> _converters = null) : base(_entityList, _originPoint, _basePoint, _converters)
        {
            isVMirror = false;
            isHMirror = false;
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            var result = base.Sampler(prompts);
            if (result != SamplerStatus.OK)
            {
                return result;
            }
            if (NeedVMirror)
            {
                VerticalMirroring();
                OnSuffixChanged(new ChangeMarkSuffixEventArgs(Suffix));
            }
            if (NeedHMirror)
            {
                HorizontalMirroring();
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

        private void VerticalMirroring()
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
            using (var tr = CADProxy.Document.TransactionManager.StartTransaction())
            {
                foreach (var e in entities)
                {
                    var ent = e.ObjectId.GetObject(OpenMode.ForWrite, true) as Entity;
                    ent.Erase(false);
                    if (ent.GetType() == typeof(DBText))
                    {
                        ent.TransformBy(Matrix3d.Displacement(new Vector3d((isVMirror ? textWidth : -textWidth), 0, 0)));
                    }
                    else
                    {
                        ent.TransformBy(Matrix3d.Mirroring(new Line3d(basePoint, new Vector3d(0, 1, 0))));
                    }
                    ent.Erase();
                }
                tr.Commit();
            }
            foreach (var ent in buffer)
            {
                if (ent.GetType() == typeof(DBText) || ent.GetType() == typeof(AttributeDefinition))
                {
                    ent.TransformBy(Matrix3d.Displacement(new Vector3d((isVMirror ? textWidth : -textWidth) / AppSettings.Get.ScaleFactor, 0, 0)));
                }
                else
                {
                    ent.TransformBy(Matrix3d.Mirroring(new Line3d(new Point3d(0, 0, 0), new Vector3d(0, 1, 0))));
                }
            }
            isVMirror = !isVMirror;
        }

        private void HorizontalMirroring()
        {
            using (var tr = CADProxy.Document.TransactionManager.StartTransaction())
            {
                foreach (var e in entities)
                {
                    var ent = e.ObjectId.GetObject(OpenMode.ForWrite, true) as Entity;
                    ent.Erase(false);
                    if (ent.GetType() == typeof(DBText))
                    {
                        ent.TransformBy(Matrix3d.Displacement(new Vector3d(0, (isHMirror ? 9 : -9) * AppSettings.Get.ScaleFactor, 0)));
                    }
                    else
                    {
                        ent.TransformBy(Matrix3d.Mirroring(new Line3d(basePoint, new Vector3d(1, 0, 0))));
                    }
                    ent.Erase();
                }
                tr.Commit();
            }
            foreach(var ent in buffer)
            {
                if (ent.GetType() == typeof(DBText) || ent.GetType() == typeof(AttributeDefinition))
                {
                    ent.TransformBy(Matrix3d.Displacement(new Vector3d(0, (isHMirror ? 9 : -9), 0)));
                }
                else
                {
                    ent.TransformBy(Matrix3d.Mirroring(new Line3d(new Point3d(0, 0, 0), new Vector3d(1, 0, 0))));
                }
            }
            isHMirror = !isHMirror;
        }

        protected override void OnSuffixChanged(ChangeMarkSuffixEventArgs _args)
        {
            base.OnSuffixChanged(_args);
        }

        private bool NeedVMirror => (currentPoint.X < basePoint.X && !isVMirror) || (currentPoint.X >= basePoint.X && isVMirror);

        private bool NeedHMirror => (currentPoint.Y < basePoint.Y && !isHMirror) || (currentPoint.Y >= basePoint.Y && isHMirror);
    }
}
