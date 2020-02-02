using CADKit.Services;
using CADKitBasic.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ElevationMark
{
    public class TestMarkTypeService : IClassFixture<IoCContainerFixture>
    {
        [Fact]
        public void mark_type_service_get_marks_return_mark_type()
        {
            IMarkTypeService service = new MarkTypeServiceStd01(new IconServiceStd01(new InterfaceSchemeService()));
            Type type = service.GetMarkType(1);
            
            Assert.Equal(Type.GetType("CADKitElevationMarks.Models.ConstructionElevationMarkCADKit"),type);
        }
    }
}
