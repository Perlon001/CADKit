﻿using CADProxy;
using CADProxy.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

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

namespace CADKit.Utils
{
    public abstract class EntityListJig : DrawJig
    {
        protected readonly Point3d basePoint;
        protected Point3d currentPoint;
        protected IEnumerable<Entity> entityList;
        protected Matrix3d transforms;

        protected EntityListJig(IEnumerable<Entity> _entityList, Point3d _basePoint) : base()
        {
            basePoint = _basePoint;
            currentPoint = _basePoint;
            //var jigGroup = _entityList.Clone().ToGroup();

            //entityList = jigGroup.ToEnumerable().Clone().ToList();
            entityList = _entityList.Clone().ToList();
            
            // tu jeszcze zamienic atrybut na text !!!
            // i mozna wywalic jigGroup :)

            entityList.TransformBy(Matrix3d.Displacement(new Vector3d( basePoint.X, basePoint.Y, basePoint.Z)));
            //foreach (var id in jigGroup.GetAllEntityIds())
            //{
            //    if (!id.IsErased)
            //    {
            //        id.GetObject(OpenMode.ForWrite).Erase();
            //    }
            //}
            //jigGroup.Erase(true);
        }

        public virtual string GetSuffix()
        {
            return "";
        }

        public virtual IEnumerable<Entity> GetEntity()
        {
            entityList.TransformBy(transforms);

            return entityList;
        }

        public Point3d Origin { get { return currentPoint; } }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            JigPromptPointOptions jigOpt = new JigPromptPointOptions("Wskaż punkt wstawienia:");
            jigOpt.UserInputControls = UserInputControls.Accept3dCoordinates;
            jigOpt.BasePoint = basePoint;
            PromptPointResult res = prompts.AcquirePoint(jigOpt);
            currentPoint = res.Value;

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
