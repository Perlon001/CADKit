using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xUnitTest.Conventions
{
    public class naming_conventions
    {
        [Fact]
        public void each_interface_name_starts_with_capital_I()
        {
            var interfaces = ConventionsHelper.Interfaces("CADKitZwCAD");

            Assert.NotEmpty(interfaces);

            var interfacesNotStartingWithI = interfaces
                .Where(x => x.Name.StartsWith("I", StringComparison.CurrentCulture) == false);

            Assert.Empty(interfacesNotStartingWithI);
        }
    }
}
