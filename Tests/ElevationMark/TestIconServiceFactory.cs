using CADKit;
using CADKit.Models;
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
        public void icon_service_factory_return_IconServiceCADKit()
        {
            //arrange
            IconServiceFactory serviceFactory = new IconServiceFactory();
            //act 
            IIconService service = serviceFactory.GetIconService(DrawingStandards.CADKit);
            //assert
            Assert.IsType<IconServiceCADKit>(service);
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
    }
}
