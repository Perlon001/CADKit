using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.GraphicsInterface;

namespace CADKitElevationMarks
{
    public class PlacementJig : DrawJig
    {
        Point3d basePoint, dragPoint;
        IEnumerable<Entity> entities;

        public Matrix3d Displacement { get; private set; }

        public PlacementJig(IEnumerable<Entity> _entities, Point3d _basePoint)
        {
            this.entities = _entities;
            this.basePoint = _basePoint;
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            var options = new JigPromptPointOptions("\nWskaz lokalizację: ");
            options.BasePoint = basePoint;
            options.UseBasePoint = true;
            options.Cursor = CursorType.RubberBand;
            options.UserInputControls = UserInputControls.Accept3dCoordinates;
            var result = prompts.AcquirePoint(options);
            if (result.Value.IsEqualTo(dragPoint))
                return SamplerStatus.NoChange;
            dragPoint = result.Value;

            return SamplerStatus.OK;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            Point3d destPoint = new Point3d(dragPoint.X, basePoint.Y, dragPoint.Z);
            Displacement = Matrix3d.Displacement(basePoint.GetVectorTo(destPoint));
            var geometry = draw.Geometry;
            if (geometry != null)
            {
                geometry.PushModelTransform(Displacement);
                foreach (var entity in entities)
                {
                    geometry.Draw(entity);
                }
                geometry.PopModelTransform();
            }
            return true;
        }
    }
}
