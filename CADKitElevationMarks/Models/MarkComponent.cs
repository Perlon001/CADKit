using CADKit.Contracts;
using CADKit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace CADKitElevationMarks.Models
{
    public class MarkComponent : Component, IComponent
    {
        public MarkComponent(string _name) : base(_name)
        {
            Properties = new Dictionary<string, object>();
        }
        public Entity Entity { get; set; }
        public Dictionary<string,object> Properties { get; private set; }
    }
}
