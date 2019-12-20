using CADKit.Models;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Models;
using CADKitElevationMarks.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ElevationMark
{
    public class TestIconServiceFactory
    {
        [Fact]
        public void IconServiceCADKitFactory_schould_create_IconServiceCADKit()
        {
            // arrange
            IconServiceFactory factory = new IconServiceFactoryCADKit();
            // act
            IIconService service = factory.CreateService();
            // assert
            Assert.NotNull(service);
            Assert.Equal(typeof(IconServiceCADKit), service.GetType());
        }

        [Fact]
        public void IconServicePNB010125Factory_schould_create_IconServicePNB01025()
        {
            // arrange
            IconServiceFactory factory = new IconServiceFactoryPNB01025();
            // act
            IIconService service = factory.CreateService();
            // assert
            Assert.NotNull(service);
            Assert.Equal(typeof(IconServicePNB01025), service.GetType());
        }
    }
}
