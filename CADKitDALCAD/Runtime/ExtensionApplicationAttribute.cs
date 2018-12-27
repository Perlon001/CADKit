using CADKit.ServiceCAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitDALCAD.Runtime
{
    public sealed class ExtensionApplicationAttribute : Attribute
    {
        private dynamic extensionApplicationAttribute;

        public ExtensionApplicationAttribute(Type entryPointType)
        {
            switch(CADEnvironment.Instance.Platform)
            {
                case CADPlatforms.ZwCAD:
                    extensionApplicationAttribute = new ZwSoft.ZwCAD.Runtime.ExtensionApplicationAttribute(entryPointType);
                    break;
                case CADPlatforms.AutoCAD:
                    extensionApplicationAttribute = new Autodesk.AutoCAD.Runtime.ExtensionApplicationAttribute(entryPointType);
                    break;
            }
        }

        public Type Type
        {
            get
            {
                return extensionApplicationAttribute.Type;
            }
        }

    }
}
