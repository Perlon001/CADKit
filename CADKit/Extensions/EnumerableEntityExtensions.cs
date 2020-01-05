using CADProxy;
using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
#endif

namespace CADKit.Extensions
{
    public static class EnumerableEntityExtensions
    {
        public static ObjectId ToBlock(this IEnumerable<Entity> _entityList, string _blockName, Point3d _origin)
        {
            ObjectId result;
            using (var tr = ProxyCAD.Document.TransactionManager.StartTransaction())
            {
                var bt = tr.GetObject(ProxyCAD.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                if (!bt.Has(_blockName))
                {
                    var i = 0;
                    using (var btr = new BlockTableRecord
                    {
                        Name = _blockName,
                        Origin = _origin
                    })
                    {
                        foreach (var e in _entityList)
                        {
                            if (e.GetType() == typeof(DBText))
                            {
                                var att = new AttributeDefinition();
                                att.Tag = "att" + i.ToString();
                                att.Prompt = "att" + i.ToString();
                                att.TextString = ((DBText)e).TextString;
                                att.Height = ((DBText)e).Height;
                                att.Position = ((DBText)e).Position;
                                att.Justify = ((DBText)e).Justify;
                                att.AlignmentPoint = ((DBText)e).AlignmentPoint;
                                btr.AppendEntity(att);
                            }
                            else
                            {
                                btr.AppendEntity(e);
                            }
                        }
                        bt.UpgradeOpen();
                        bt.Add(btr);
                        tr.AddNewlyCreatedDBObject(btr, true);
                        tr.Commit();
                    }
                }
                result = bt[_blockName];
            }
            
            return result;

            //using(var tr = ProxyCAD.Document.TransactionManager.StartTransaction())
            //{
            //    var bt = tr.GetObject(ProxyCAD.Database.BlockTableId, OpenMode.ForWrite) as BlockTable;
            //    if (!bt.Has(_blockName))
            //    {
            //        using (var btr = new BlockTableRecord())
            //        {
            //            var i = 0;
            //            btr.Name = _blockName;
            //            btr.Origin = new Point3d(0, 0, 0);
            //            foreach(var e in _entityList)
            //            {
            //                if(e.GetType() == typeof(DBText))
            //                {
            //                    i++;
            //                    using(var att = new AttributeDefinition())
            //                    {
            //                        att.Position = ((DBText)e).AlignmentPoint;
            //                        att.Verifiable = true;
            //                        att.Prompt = "att"+i.ToString();
            //                        att.Tag = "att" + i.ToString();
            //                        att.TextString = ((DBText)e).TextString;
            //                        att.Height = ((DBText)e).Height;
            //                        att.Justify = ((DBText)e).Justify;

            //                        btr.AppendEntity(att);

            //                    }
            //                }
            //                else
            //                {
            //                    btr.AppendEntity(e);
            //                }
            //            }
            //            // bt.UpgradeOpen();
            //            bt.Add(btr);
            //            tr.AddNewlyCreatedDBObject(btr, true);
            //        }
            //    }
            //    tr.Commit();
            //} 
        }

        public static Group ToGroup(this IEnumerable<Entity> _entityList)
        {
            Group group = new Group();
            using (var tr = ProxyCAD.Document.TransactionManager.StartTransaction())
            {
                DBDictionary groupDictionary = tr.GetObject(ProxyCAD.Database.GroupDictionaryId, OpenMode.ForWrite) as DBDictionary;
                groupDictionary.SetAt("*", group);
                BlockTable bt = tr.GetObject(ProxyCAD.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                group = ToGroup(_entityList, tr, btr, group);
                tr.AddNewlyCreatedDBObject(group, true);
                tr.Commit();
            }

            return group;
        }

        public static Group ToGroup(this IEnumerable<Entity> _entityList, Group _gr)
        {
            using (var tr = ProxyCAD.Document.TransactionManager.StartTransaction())
            {
                BlockTable bt = tr.GetObject(ProxyCAD.Database.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = tr.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                _gr = ToGroup(_entityList, tr, btr, _gr);
                tr.Commit();
            }

            return _gr;
        }

        public static Group ToGroup(this IEnumerable<Entity> _entityList, Transaction _tr, BlockTableRecord _btr)
        {
            Group group = new Group();
            DBDictionary groupDictionary = _tr.GetObject(ProxyCAD.Database.GroupDictionaryId, OpenMode.ForWrite) as DBDictionary;
            groupDictionary.SetAt("*", group);
            group = ToGroup(_entityList, _tr, _btr, group);
            _tr.AddNewlyCreatedDBObject(group, true);

            return group;
        }

        public static Group ToGroup(this IEnumerable<Entity> _entityList, Transaction _tr, BlockTableRecord _btr, Group _gr)
        {
            foreach (var ent in _entityList)
            {
                _btr.AppendEntity(ent);
                _tr.AddNewlyCreatedDBObject(ent, true);
                _gr.Append(ent.ObjectId);
            }

            return _gr;
        }

        public static IEnumerable<Entity> TransformBy(this IEnumerable<Entity> _entityList, Matrix3d _matrix)
        {
            foreach (var e in _entityList)
            {
                e.TransformBy(_matrix);
            }

            return _entityList;
        }
    }
}
