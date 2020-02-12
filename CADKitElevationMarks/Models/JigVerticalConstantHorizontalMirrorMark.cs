using CADKit;
using System;
using System.Collections.Generic;
using CADKit.Proxy;
using CADKit.Contracts;

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
    public class JigVerticalConstantHorizontalMirrorMark : JigMark
    {
        private bool IsHMirror;
        public JigVerticalConstantHorizontalMirrorMark(IEnumerable<Entity> _entityList, Point3d _basePoint, IEnumerable<IEntityConverter> _converters) : base(_entityList, _basePoint, _converters)
        {
            IsHMirror = false;
        }

        public override string GetSuffix()
        {
            return (IsHMirror ? "B" : "T");
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            var result = base.Sampler(prompts);
            if ( result != SamplerStatus.OK) 
                return result;
            if (NeedHMirror)
            {
                HorizontalMirroring();
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
                        ent.TransformBy(Matrix3d.Displacement(new Vector3d(0, (IsHMirror ? 9 : -9) * AppSettings.Get.ScaleFactor, 0)));
                    }
                    else
                    {
                        ent.TransformBy(Matrix3d.Mirroring(new Line3d(basePoint, new Vector3d(1, 0, 0))));
                    }
                    ent.Erase();
                }
                tr.Commit();
            }
            foreach (var ent in buffer)
            {
                if (ent.GetType() == typeof(DBText) || ent.GetType() == typeof(AttributeDefinition))
                {
                    ent.TransformBy(Matrix3d.Displacement(new Vector3d(0, (IsHMirror ? 9 : -9), 0)));
                }
                else
                {
                    ent.TransformBy(Matrix3d.Mirroring(new Line3d(new Point3d(0, 0, 0), new Vector3d(1, 0, 0))));
                }
            }
            IsHMirror = !IsHMirror;
        }

        private bool NeedHMirror => (currentPoint.Y < basePoint.Y && !IsHMirror) || (currentPoint.Y >= basePoint.Y && IsHMirror);
    }
}
