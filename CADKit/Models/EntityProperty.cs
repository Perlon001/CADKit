using CADKit.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Models
{
    public class EntityProperty : Component, IComponent
    {
        public object Value { get; private set; }

        public EntityProperty(string _name, object _value) : base(_name)
        {
            Value = _value;
        }
    }
}
