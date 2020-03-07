using CADKit.Contracts;
using System.Collections.Generic;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKit.Models
{
    public abstract class EntityComposite : Composite, IEntityComposite
    {
        protected new readonly ICollection<IComponent> components = new List<IComponent>();
        protected EntityComposite(string _name) : base(_name)
        {
            Properties = new Dictionary<string, object>();
        }

        public Dictionary<string, object> Properties { get; protected set; }

        public Entity Entity { get; protected set; }
    }
}
