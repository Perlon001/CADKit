using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CADKit.Models;
using CADKit.Proxy.DatabaseServices;
using NSubstitute;
using Xunit;
namespace Tests
{
    public class TestEntitiesSetCreator
    {
        [Fact]
        public void BuilderShuldByReturnEntitiesSetCreator()
        {
            EntitiesSet es = new EntitiesSetBuilder(new List<Entity>())
                .Build();

            Assert.IsType<EntitiesSet>(es);

        }
    }
}
