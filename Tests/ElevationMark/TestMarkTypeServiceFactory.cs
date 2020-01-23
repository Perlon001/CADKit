using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using CADKitElevationMarks.Services;
using NSubstitute;
using Xunit;

namespace Tests.ElevationMark
{
    public class TestMarkTypeServiceFactory : IClassFixture<IoCContainerFixture>
    {
        public TestMarkTypeServiceFactory()
        {
            
        }

        [Fact]
        public void mark_type_service_factory_throw_exception_when_drawing_standard_not_recognized()
        {
            //arrange
            MarkTypeServiceFactory serviceFactory = new MarkTypeServiceFactory();

            // act 
            //NotSupportedException ex = Assert.Throws<NotSupportedException>(() => serviceFactory.GetMarkTypeService(DrawingStandards.none));

            //assert
            //Assert.NotNull(ex);
        }

        [Fact]
        public void mark_type_service_factory_return_service()
        {
            //arrange
            MarkTypeServiceFactory serviceFactory = new MarkTypeServiceFactory();
            //act 
            IMarkTypeService service = serviceFactory.GetMarkTypeService(Arg.Any<DrawingStandards>());
            //assert
            Assert.NotNull(service);
        }

        [Fact]
        public void mark_type_service_factory_return_MarkTypeServiceCADKit()
        {
            //arrange
            MarkTypeServiceFactory serviceFactory = new MarkTypeServiceFactory();
            //act 
            IMarkTypeService service = serviceFactory.GetMarkTypeService(DrawingStandards.Std01);
            //assert
            Assert.IsType<MarkTypeServiceStd01>(service);
        }

        [Fact]
        public void mark_type_service_factory_return_MarkTypeServicePNB01025()
        {
            //arrange
            MarkTypeServiceFactory serviceFactory = new MarkTypeServiceFactory();
            //act 
            IMarkTypeService service = serviceFactory.GetMarkTypeService(DrawingStandards.PNB01025);
            //assert
            Assert.IsType<MarkTypeServicePNB01025>(service);
        }
    }
}
