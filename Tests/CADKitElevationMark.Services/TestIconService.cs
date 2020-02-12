using CADKit.Contracts;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Services;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ElevationMark
{
    public class TestIconService
    {
        [Fact]
        public void service_should_return_default_icon()
        {
            var iconService = Substitute.For<IInterfaceSchemeService>();
            IMarkIconService service = new MarkIconService();
            var icon = service.GetIcon(DrawingStandards.PNB01025, MarkTypes.universal);
        }
    }
}
