using CADKit.Contracts;
using CADKitBasic.Contracts.Services;
using CADKit.Models;
using CADKitBasic.Services;
using NSubstitute;
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
            Assert.Collection<IComposite>(modules, item => Assert.Contains("markModule", item.Name));
            Assert.Collection<IComposite>(modules, item => Assert.Contains("Koty wysokościowe", item.Title));

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
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "kota01"));
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "kota03"));
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "kota03"));
        }

        [Fact]
        public void get_composites_return_valid_composites_list_from_composite()
        {
            var service = new CompositeService(new LocalFakeCompositeProvider());
            var item = service.GetComposites("markModule").FirstOrDefault(a => a.Name == "kota01");
            var composites = service.GetComposites(item);

            Assert.True(composites.Count == 4);
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "contourLine01"));
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "contourFill"));
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "markSign"));
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "markValue"));

            item = service.GetComposites("markModule").FirstOrDefault(a => a.Name == "kota02");
            composites = service.GetComposites(item);
            Assert.True(composites.Count == 3);
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "contourLine01"));
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "markSign"));
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "markValue"));

            item = service.GetComposites("markModule").FirstOrDefault(a => a.Name == "kota03");
            composites = service.GetComposites(item);
            Assert.True(composites.Count == 4);
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "contourLine01"));
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "contourLine02"));
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "markSign"));
            Assert.NotNull(composites.FirstOrDefault(a => a.Name == "markValue"));
        }

        [Fact]
        public void get_composite_return_valid_composites_from_module()
        {
            var service = new CompositeService(new LocalFakeCompositeProvider());
            var item = service.GetComposite("markModule","kota01");

            string expected = "Architektoniczna kota wysokościowa";
            string result = item.Title;
            Assert.NotNull(item);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void get_composite_return_valid_composites_from_composite()
        {
            ICompositeProvider compositeProvider = new LocalFakeCompositeProvider();
            var service = new CompositeService(compositeProvider);
            // var service = new CompositeService(Substitute.For<ICompositeProvider>());


            var item = service.GetComposite("markModule", "kota01");
            var subitem = item.GetComponent("contourLine01");

            string expected = "Linia konturowa koty";
            string result = subitem.Title;

            Assert.NotNull(subitem);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void get_acces_path_return_valid_path()
        {
            var service = new CompositeService(new LocalFakeCompositeProvider());
            IComponent composite = service.GetComposite("markModule", "kota01").GetComponent("contourFill");

            var path = service.GetAccessPath((IComposite)composite);

            Assert.Equal(3, path.Count);
            Assert.Equal("markModule", path[0]);
            Assert.Equal("kota01", path[1]);
            Assert.Equal("contourFill", path[2]);
        }
    }
}
