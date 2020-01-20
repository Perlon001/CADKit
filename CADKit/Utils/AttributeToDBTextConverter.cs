using CADKit.Contracts.Services;
using CADProxy;
using System.Collections.Generic;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKit.Utils
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
                    result.Add(ProxyCAD.ToDBText((AttributeDefinition)item));
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
