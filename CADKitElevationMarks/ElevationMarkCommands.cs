using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.GraphicsInterface;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Runtime;

namespace JigTest
{
    public class Jig : DrawJig
    {
        private readonly Point3d basePoint;
        private readonly Entity entity;
        private Point3d currentPoint;
        private Matrix3d transformation;

        public Jig(Entity txt, Point3d point) : base()
        {
            entity = txt;
            basePoint = new Point3d(0, 0, 0);
        }

        public Entity GetEntity()
        {
            var result = entity.Clone() as Entity;
            result.TransformBy(transformation);

            return result;
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {
            var jigOpt = new JigPromptPointOptions("Wskaż punkt wstawienia:")
            {
                UserInputControls = UserInputControls.Accept3dCoordinates,
                BasePoint = basePoint
            };
            var res = prompts.AcquirePoint(jigOpt);
            currentPoint = res.Value;

            return SamplerStatus.OK;
        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            transformation = Matrix3d.Displacement(basePoint.GetVectorTo(currentPoint));
            var geometry = draw.Geometry;
            if (geometry != null)
            {
                geometry.PushModelTransform(transformation);
                geometry.Draw(entity);
            }

            return true;
        }
    }

    public class JigCommands
    {
        [CommandMethod("JIG")]
        public void Jig()
        {
            try
            {
                var tx1 = new DBText();
                tx1.SetDatabaseDefaults();
                tx1.Height = 2;
                tx1.Position = new Point3d(0, 0, 0);
                tx1.TextString = "Tekst";
                tx1.VerticalMode = TextVerticalMode.TextVerticalMid;

                var jig = new Jig(tx1, new Point3d(0, 0, 0));
                var result = Application.DocumentManager.MdiActiveDocument.Editor.Drag(jig);
                if (result.Status == PromptStatus.OK)
                {
                    SaveToDatabase(jig.GetEntity());
                }
            }
            catch (Exception ex)
            {
                Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage(ex.Message);
            }
        }

        public static void SaveToDatabase(Entity ent)
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                var btr = tr.GetObject(doc.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                btr.AppendEntity(ent);
                tr.AddNewlyCreatedDBObject(ent, true);
                tr.Commit();
            }
        }
        public static void EraseFromDatabase(Entity ent)
        {
            using (var tr = Application.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction())
            {
                ent.ObjectId.GetObject(OpenMode.ForWrite).Erase(true);
                tr.Commit();
            }
        }
    }

    public static class Extensions
    {
        public static void Flush(this DBText txt)
        {
            var doc = Application.DocumentManager.MdiActiveDocument;
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                var btr = tr.GetObject(doc.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                btr.AppendEntity(txt);
                tr.AddNewlyCreatedDBObject(txt, true);
                txt.Erase();
                tr.Commit();
            }
        }
    }
}
