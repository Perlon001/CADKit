using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Conventions;
using Xunit;

namespace Test.Conventions
{
    public class naming_conventions
    {
        [Fact]
        public void each_interface_cadkit_database_name_starts_with_capital_I()
        {
            var interfaces = ConventionsHelper.Interfaces("CADKit.Database");

            Assert.NotEmpty(interfaces);

            var interfacesNotStartingWithI = interfaces
                .Where(x => x.Name.StartsWith("I", StringComparison.CurrentCulture) == false);

            Assert.Empty(interfacesNotStartingWithI);
        }

        [Fact]
        public void each_interface_cadkit_starts_with_capital_I()
        {
            var interfaces = ConventionsHelper.Interfaces("CADKit");

            Assert.NotEmpty(interfaces);

            var interfacesNotStartingWithI = interfaces
                .Where(x => x.Name.StartsWith("I", StringComparison.CurrentCulture) == false);

            Assert.Empty(interfacesNotStartingWithI);
        }

    }
}
