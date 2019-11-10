﻿using CADProxy;
using System;
#if ZwCAD
using ZwSoft.ZwCAD.Runtime;
#endif
#if AutoCAD
using Autodesk.AutoCAD.Runtime;
#endif

namespace CADKitElevationMarks
{
    public class Autostart : IExtensionApplication
    {
        public void Initialize()
        {
            ProxyCAD.Editor.WriteMessage("\nAutostart CADKitElevationMarks...");
        }

        public void Terminate()
        {
            throw new NotImplementedException();
        }
    }
}
