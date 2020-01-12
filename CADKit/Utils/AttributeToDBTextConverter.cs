using CADKit.Contracts.Services;
using CADProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKit.Utils
{
    public class AttributeToDBTextConverter : IEntityConvert
    {
        public IEnumerable<Entity> Convert(IEnumerable<Entity> _entities)
        {
            IList<Entity> result = new List<Entity>();
            foreach(var item in _entities)
            {
                if(item.GetType() == typeof(AttributeDefinition))
                {
                    result.Add(ProxyCAD.ToDBText(item as AttributeDefinition));
                }
                else
                {
                    result.Add(item);
                }
            }

            return result as IEnumerable<Entity>;
        }
    }
}
