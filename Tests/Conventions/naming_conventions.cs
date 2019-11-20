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
        public void each_interface_cadkit_name_starts_with_capital_I()
        {
            var interfaces = ConventionsHelper.Interfaces("CADKit");

            Assert.NotEmpty(interfaces);

            var interfacesNotStartingWithI = interfaces
                .Where(x => x.Name.StartsWith("I", StringComparison.CurrentCulture) == false);

            Assert.Empty(interfacesNotStartingWithI);
        }

        [Fact]
        public void each_interface_cadproxy_name_starts_with_capital_I()
        {

            var assemblies = ConventionsHelper.Assemblies("CADProxyZwcad.dll");
            var interfaces = ConventionsHelper.Interfaces("CADProxyZwCAD.dll");

            Assert.NotEmpty(interfaces);

            var interfacesNotStartingWithI = interfaces
                .Where(x => x.Name.StartsWith("I", StringComparison.CurrentCulture) == false);

            Assert.Empty(interfacesNotStartingWithI);
        }

        [Fact]
        public void each_interface_cadkit_database_cad_starts_with_capital_I()
        {
            var interfaces = ConventionsHelper.Interfaces("CADKit.Database");

            Assert.NotEmpty(interfaces);

            var interfacesNotStartingWithI = interfaces
                .Where(x => x.Name.StartsWith("I", StringComparison.CurrentCulture) == false);

            Assert.Empty(interfacesNotStartingWithI);
        }

    }
}
