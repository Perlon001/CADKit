using CADKitBasic.Contracts.Services;
using CADKit;
using System.Collections.Generic;
using CADKit.Proxy;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKitBasic.Utils
{
    public class AttributeToDBTextConverter : IEntityConverter
    {
        public IEnumerable<Entity> Convert(IEnumerable<Entity> _entities)
        {
            IList<Entity> result = new List<Entity>();
            foreach (var item in _entities)
            {
                if (true && item.GetType().Equals(typeof(AttributeDefinition)))
                {
                    result.Add(CADProxy.ToDBText((AttributeDefinition)item));
                }
                else
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
