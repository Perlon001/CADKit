using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Services;
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
            IIconService service = new MarkIconService();
        }
    }
}
