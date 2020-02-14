using CADKit;
using System;
using System.Reflection;

namespace Tests
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<Pending>")]
    public class IoCContainerFixture : IDisposable 
    {
        public IoCContainerFixture()
        {
            Assembly.LoadFrom(@"C:\Program Files\ZWSOFT\ZWCAD 2020\ZwDatabaseMgd.DLL");
            Assembly.LoadFrom(@"C:\Program Files\ZWSOFT\ZWCAD 2020\ZwManaged.dll");
            DI.Container = Container.Builder.Build();
        }
        public void Dispose()
        {
            DI.Container.Dispose();
        }
    }
}
