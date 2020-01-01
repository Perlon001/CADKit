﻿using CADKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<Pending>")]
    public class IoCContainerFixture : IDisposable
    {
        public IoCContainerFixture()
        {
            Assembly.LoadFrom(@"C:\Program Files\ZWSOFT\ZWCAD 2020\ZwDatabaseMgd.dll");
            Assembly.LoadFrom(@"C:\Program Files\ZWSOFT\ZWCAD 2020\ZwManaged.dll");
            // DI.Container = Container.Builder.Build();
        }
        public void Dispose()
        {
            // DI.Container.Dispose();
        }
    }
}