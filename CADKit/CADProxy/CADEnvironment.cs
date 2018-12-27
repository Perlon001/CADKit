using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitDALCAD
{
    public sealed class CADEnvironment
    {
        private static CADEnvironment instance = null;
        private CADPlatforms platform;

        public static CADEnvironment Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CADEnvironment();
                    instance.platform = CADPlatforms.ZwCAD;
                }
                return instance;
            }
        }

        public CADPlatforms Platform
        {
            get
            {
                return platform;
            }
        }
    }
}
