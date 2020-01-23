using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if ZwCAD
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif
#if AutoCAD
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKit.Extensions
{
    public static class GroupExtensions
    {
        public static IEnumerable<Entity> ToEnumerable(this Group _group)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                return _group.GetAllEntityIds().Select(x => x.GetObject(OpenMode.ForRead) as Entity);
            }
        }

        public static BlockTableRecord ToBlock(this Group _group, string _blockName, Point3d _origin)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                var bt = tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = new BlockTableRecord()
                {
                    Name = _blockName,
                    Origin = _origin
                };
                if (!bt.Has(_blockName))
                {
                    _group.ToEnumerable().ForEach(x => btr.AppendEntity(x));
                    //foreach (var e in _group.ToEnumerable())
                    //{
                    //    btr.AppendEntity(e);
                    //}
                    bt.UpgradeOpen();
                    bt.Add(btr);
                    tr.AddNewlyCreatedDBObject(btr, true);
                    tr.Commit();
                }
                return btr;
            }
        }

    }

}
