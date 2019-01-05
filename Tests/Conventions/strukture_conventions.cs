using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Conventions;
using Xunit;

namespace Test.Conventions
{
    public class strukture_conventions
    {

        [Fact]
        public void each_class_in_Repository_namespace_implements_interfaces()
        {
            var repositorys = ConventionsHelper.Classes("SODP.Repository")
                .Where(x => x.Namespace.EndsWith(".Repository", StringComparison.CurrentCulture));

            Assert.NotEmpty(repositorys);

            var repositoryClassesWithoutInterfaces = repositorys
                .Where(x => x.GetInterfaces().Length == 0);

            Assert.Empty(repositoryClassesWithoutInterfaces);
        }

        [Fact]
        public void each_class_in_CoreController_namespace_implements_interfaces()
        {
            var controllers = ConventionsHelper.Classes("SODP.Core")
                .Where(x => x.Namespace.EndsWith(".Controller", StringComparison.CurrentCulture));

            Assert.NotEmpty(controllers);

            var controllerClassesWithoutInterfaces = controllers
                .Where(x => x.GetInterfaces().Length == 0);

            Assert.Empty(controllerClassesWithoutInterfaces);
        }
    }
}
