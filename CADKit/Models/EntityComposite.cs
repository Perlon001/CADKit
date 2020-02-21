using CADKit.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKit.Models
{
    public abstract class EntityComposite : Composite, IEntityComposite
    {
        protected EntityComposite(string _name) : base(_name)
        {
        }

        public Dictionary<string, object> Properties { get; protected set; }

        public Entity Entity { get; protected set; }
    }
}
