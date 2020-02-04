using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using CADKitElevationMarks.Services;
using NSubstitute;
using Xunit;

namespace Tests.ElevationMark
{
    public class TestIconServiceFactory : IClassFixture<IoCContainerFixture>
    {
        [Fact]
        public void icon_service_factory_throw_exception_when_drawing_standard_not_recognized()
        {
            //arrange
            IconServiceFactory serviceFactory = new IconServiceFactory();
        
            // act 
            //NotSupportedException ex = Assert.Throws<NotSupportedException>(() => serviceFactory.GetIconService(DrawingStandards.none));

            //assert
            //Assert.NotNull(ex);
        }

        [Fact]
        public void icon_service_factory_return_service()
        {
            //arrange
            IconServiceFactory serviceFactory = new IconServiceFactory();
            //act 
            IIconService service = serviceFactory.GetIconService(Arg.Any<DrawingStandards>());
            //assert
            Assert.NotNull(service);
        }

        [Fact]
        public void icon_service_factory_return_IconServiceStd01()
        {
            //arrange
            IconServiceFactory serviceFactory = new IconServiceFactory();
            //act 
            IIconService service = serviceFactory.GetIconService(DrawingStandards.Std01);
            //assert
            Assert.IsType<IconServiceStd01>(service);
        }

        [Fact]
        public void icon_service_factory_return_IconServicePNB010250()
        {
            //arrange
            IconServiceFactory serviceFactory = new IconServiceFactory();
            //act 
            IIconService service = serviceFactory.GetIconService(DrawingStandards.PNB01025);
            //assert
            Assert.IsType<IconServicePNB01025>(service);
        }

        [Fact]
        public void icon_service_factory_return_IconServiceDefault()
        {
            //arrange
            IconServiceFactory serviceFactory = new IconServiceFactory();
            //act 
            // IIconService service = serviceFactory.GetIconService(DrawingStandards.Std02);
            //assert
            // Assert.IsType<IconServiceDefault>(service);
        }

    }
}
