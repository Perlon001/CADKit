using CADKit.Contracts;
using CADKit.Models;
using CADKit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.CoreServices
{
    public class CompositesService
    {
        [Fact]
        public void get_composite_module_list_return_valid_module()
        {
            var provider = new LocalFakeCompositeProvider();
            var modules = provider.GetModules();

            // testy provider'a do przeniesienia do innego testu
            Assert.Collection<Composite>(modules, item => Assert.Contains("markModule", item.LeafName));
            Assert.Collection<Composite>(modules, item => Assert.Contains("Koty wysokościowe", item.LeafTitle));

            var service = new CompositeService(provider);
            IDictionary<string, string> composites = service.GetCompositeModulesList();

            Assert.True(composites.Count == 1);
            Assert.True(composites.ContainsKey("markModule"));
        }

        [Fact]
        public void get_composites_return_valid_composites_list_from_module()
        {
            var service = new CompositeService(new LocalFakeCompositeProvider());
            var composites = service.GetComposites("markModule");
            Assert.True(composites.Count == 3);
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "kota01"));
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "kota03"));
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "kota03"));
        }

        [Fact]
        public void get_composites_return_valid_composites_list_from_composite()
        {
            var service = new CompositeService(new LocalFakeCompositeProvider());
            var item = service.GetComposites("markModule").FirstOrDefault(a => a.LeafName == "kota01");
            var composites = service.GetComposites(item);

            Assert.True(composites.Count == 4);
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "contourLine01"));
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "contourFill"));
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "markSign"));
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "markValue"));

            item = service.GetComposites("markModule").FirstOrDefault(a => a.LeafName == "kota02");
            composites = service.GetComposites(item);
            Assert.True(composites.Count == 3);
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "contourLine01"));
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "markSign"));
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "markValue"));

            item = service.GetComposites("markModule").FirstOrDefault(a => a.LeafName == "kota03");
            composites = service.GetComposites(item);
            Assert.True(composites.Count == 4);
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "contourLine01"));
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "contourLine02"));
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "markSign"));
            Assert.NotNull(composites.FirstOrDefault(a => a.LeafName == "markValue"));
        }

        [Fact]
        public void get_composite_return_valid_composites_from_module()
        {
            var service = new CompositeService(new LocalFakeCompositeProvider());
            var item = service.GetComposite("markModule","kota01");

            string expected = "Architektoniczna kota wysokościowa";
            string result = item.LeafTitle;
            Assert.NotNull(item);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void get_composite_return_valid_composites_from_composite()
        {
            var service = new CompositeService(new LocalFakeCompositeProvider());
            var item = service.GetComposite("markModule", "kota01");
            var subitem = item.GetLeaf("contourLine01");

            string expected = "Linia konturowa koty";
            string result = subitem.LeafTitle;

            Assert.NotNull(subitem);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void get_acces_path_return_valid_path()
        {
            var service = new CompositeService(new LocalFakeCompositeProvider());
            IComponent composite = service.GetComposite("markModule", "kota01").GetLeaf("contourFill");

            var path = service.GetAccessPath((Composite)composite);

            Assert.Equal(3, path.Count);
            Assert.Equal("markModule", path[0]);
            Assert.Equal("kota01", path[1]);
            Assert.Equal("contourFill", path[2]);
        }
    }
}
