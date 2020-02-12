using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using NSubstitute;
using CADKit.Contracts;
using CADKit.Services;

namespace Tests.CADKit.Services
{
    public class InterfaceSchemeServiceTest
    {
        [Fact]
        public void InterfaceSchemeService_shuld_by_return_()
        {
            IInterfaceSchemeService service = new InterfaceSchemeService();

            var scheme = service.GetScheme();

            Assert.True(scheme.Equals(InterfaceScheme.light) || scheme.Equals(InterfaceScheme.dark));
        }
    }
}
