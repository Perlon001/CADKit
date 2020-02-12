using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using CADKit.Contracts;
using CADKitElevationMarks.Services;
using CADKitElevationMarks.Contracts;
using System.Drawing;
using System.Reflection;
using CADKit;

namespace Tests.CADKitElevationMark.Services
{
    public class IconServiceTest
    {
        public IconServiceTest()
        {
            Assembly.LoadFrom(@"C:\Program Files\ZWSOFT\ZWCAD 2020\ZwDatabaseMgd.DLL");
            Assembly.LoadFrom(@"C:\Program Files\ZWSOFT\ZWCAD 2020\ZwManaged.dll");
            DI.Container = Container.Builder.Build();
        }

        [Fact]
        public void IconService_shuld_by_return_icon()
        {
            var iss = Substitute.For<IInterfaceSchemeService>();
            iss.GetScheme().Returns(InterfaceScheme.dark);

            var service = new MarkIconService();

            Assert.IsType<Bitmap>(service.GetIcon(DrawingStandards.PNB01025, MarkTypes.universal));
        }
    }
}
