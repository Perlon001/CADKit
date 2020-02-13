using CADKit.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;

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
        }

        public static IEnumerable<Entity> Clone(this IEnumerable<Entity> _entityList)
        {
            return _entityList.Select(x => x.Clone() as Entity);
        }

        public static void StoreToDatabase(this IEnumerable<Entity> _entities)
        {
            using(var tr = CADProxy.Document.TransactionManager.StartTransaction())
            {
                var doc = CADProxy.Document;
                var btr = tr.GetObject(doc.Database.CurrentSpaceId, OpenMode.ForWrite) as BlockTableRecord;
                foreach (var e in _entities)
                {
                    btr.AppendEntity(e);
                    tr.AddNewlyCreatedDBObject(e, true);
                }
                tr.Commit();
            }
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
                    //TODO: spersystowac definicję atrybutu zeby się prawidłowo wyświetlał
                    Entity ent = e;
                    //if (e.GetType().Equals(typeof(AttributeDefinition)))
                    //{
                    //    ent = CADProxy.ToDBText((AttributeDefinition)e);
                    //}
                    //else
                    //{
                    //    ent = e;
                    //}
                    btr.AppendEntity(ent);
                    tr.AddNewlyCreatedDBObject(ent, true);
                    gr.Append(ent.ObjectId);
                }
                tr.Commit();
            }

            return gr;
        }

        public static BlockTableRecord ToBlock(this IEnumerable<Entity> _entityList, string _blockName, Point3d _origin = default, bool redefine = false)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            using (var tr = doc.TransactionManager.StartTransaction())
            {
                BlockTableRecord btr;
                var bt = tr.GetObject(doc.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                if(bt.Has(_blockName))
                {
                    btr = bt[_blockName].GetObject(OpenMode.ForRead) as BlockTableRecord;
                }
                else
                {
                    btr = new BlockTableRecord()
                    {
                        Name = _blockName,
                        Origin = _origin
                    }; 
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
