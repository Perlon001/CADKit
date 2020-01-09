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

namespace CADProxy.Extensions
{
    public static class EnumerableEntityExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> _dict, Action<T> _action)
        {
            foreach (var item in _dict)
            {
                _action(item);
            }
        }

        public static void TransformBy(this IEnumerable<Entity> _entityList, Matrix3d _matrix)
        {
            _entityList.ForEach(x => x.TransformBy(_matrix));
            //foreach (var e in _entityList)
            //{
            //    e.TransformBy(_matrix);
            //}
        }

        public static IEnumerable<Entity> Clone(this IEnumerable<Entity> _entityList)
        {
            return _entityList.Select(x => x.Clone() as Entity);
        }

        public static Group ToGroup(this IEnumerable<Entity> _entities)
        {
            var gr = new Group();
            Document doc = Application.DocumentManager.MdiActiveDocument;
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                var dict = tr.GetObject(doc.Database.GroupDictionaryId, OpenMode.ForWrite) as DBDictionary;
                dict.SetAt("*", gr);
                tr.AddNewlyCreatedDBObject(gr, true);

                var btr = tr.GetObject(doc.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                foreach (var e in _entities)
                {
                    Entity ent;
                    if (e.GetType().Equals(typeof(AttributeDefinition)))
                    {
                        ent = ProxyCAD.ToDBText((AttributeDefinition)e);
                    }
                    else
                    {
                        ent = e;
                    }
                    btr.AppendEntity(ent);
                    tr.AddNewlyCreatedDBObject(ent, true);
                    gr.Append(ent.ObjectId);
                }
                tr.Commit();
            }

            return gr;
        }

        public static BlockTableRecord ToBlock(this IEnumerable<Entity> _entityList, string _blockName, Point3d _origin, bool redefine = false)
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
                    foreach (var e in _entityList.Clone())
                    {
                        btr.AppendEntity(e);
                    }
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
