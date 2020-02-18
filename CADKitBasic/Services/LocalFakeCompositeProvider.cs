using CADKitBasic.Contracts.Services;
using CADKit.Models;

namespace CADKitBasic.Services
{
    public class LocalFakeCompositeProvider : CompositeProvider, ICompositeProvider
    {
        public override void Load()
        {
            Composite module = new Composite()
            {
                Name = "markModule",
                Title = "Koty wysokościowe"
            };

            Composite kota;

            kota = new Composite()
            {
                Name = "kota01",
                Title = "Architektoniczna kota wysokościowa",
                Layer = "0",
                Linetype = "BYLAYER",
                ColorIndex = 256
            };
            kota.AddComponent(new Composite()
            {
                Name = "contourLine01",
                Title = "Linia konturowa koty",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 256
            });
            kota.AddComponent(new Composite()
            {
                Name = "markSign",
                Title = "Znak poziomu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 7
            });
            kota.AddComponent(new Composite()
            {
                Name = "markValue",
                Title = "Wartość poziomu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 7
            });
            module.AddComponent(kota);

            kota = new Composite()
            {
                Name = "kota02",
                Title = "Konstrukcyjna kota wysokościowa",
                Layer = "0",
                Linetype = "BYLAYER",
                ColorIndex = 256
            };
            kota.AddComponent(new Composite()
            {
                Name = "contourLine01",
                Title = "Linia konturowa koty",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 256
            });
            kota.AddComponent(new Composite()
            {
                Name = "contourFill",
                Title = "Wypełnienie grotu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 2
            });
            kota.AddComponent(new Composite()
            {
                Name = "markSign",
                Title = "Znak poziomu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 7
            });
            kota.AddComponent(new Composite()
            {
                Name = "markValue",
                Title = "Wartość poziomu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 7
            });
            module.AddComponent(kota);

            kota = new Composite()
            {
                Name = "kota03",
                Title = "Kota wysokościowa PN-B",
                Layer = "0",
                Linetype = "BYLAYER",
                ColorIndex = 256
            };
            kota.AddComponent(new Composite()
            {
                Name = "contourLine01",
                Title = "Linia konturowa koty",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 256
            });
            kota.AddComponent(new Composite()
            {
                Name = "contourLine02",
                Title = "Grot koty",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 256
            });
            kota.AddComponent(new Composite()
            {
                Name = "markSign",
                Title = "Znak poziomu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 7
            });
            kota.AddComponent(new Composite()
            {
                Name = "markValue",
                Title = "Wartość poziomu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 7
            });
            module.AddComponent(kota);

            composites.Add(module);

            module = new Composite()
            {
                Name = "markModule",
                Title = "Inne obiekty"
            };

            kota = new Composite()
            {
                Name = "Dziura",
                Title = "Obiekt dziura",
                Layer = "0",
                Linetype = "BYLAYER",
                ColorIndex = 256
            };
            kota.AddComponent(new Composite()
            {
                Name = "Element dziury",
                Title = "Nazwa elementu dziury",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 256
            });
            kota.AddComponent(new Composite()
            {
                Name = "Drugi element dziury",
                Title = "Nazwa drugiego elementu dziury",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 256
            });

            module.AddComponent(kota);

            composites.Add(module);
        }
    }
}
