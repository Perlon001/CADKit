using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public static class BlockTableRecordExtensions
    {
        public static IList<ObjectId> ToList(this BlockTableRecord _btr)
        {
            var result = new List<ObjectId>();
            foreach(ObjectId id in _btr)
            {
                result.Add(id);
            }

            return result;
        }

        public static AttributeDefinition GetAttribDefinition(this BlockTableRecord _btr, string _tag)
        {
            foreach(ObjectId id in _btr)
            {
                var attDef = id.GetObject(OpenMode.ForRead) as AttributeDefinition;
                if (attDef != null && attDef.Tag == _tag)
                {
                    return attDef;
                }
            }

            throw new NullReferenceException();
        }
    }
}
