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
    public class TestMarkTypeService
    {
        [Fact]
        public void mark_type_service_return_list_of_marks()
        {
            MarkTypeService service = new MarkTypeService();
            IList<MarkButtonDTO> result = service.GetMarks();

            Assert.NotNull(result);
        }
    }
}
