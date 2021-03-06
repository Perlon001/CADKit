﻿using CADKitBasic.Contracts.Services;
using CADKitBasic.Models;

namespace CADKitBasic.Services
{
    public class LocalFakeCompositeProvider : CompositeProvider, ICompositeProvider
    {
        public override void Load()
        {
            Composite module = new Composite()
            {
                LeafName = "markModule",
                LeafTitle = "Koty wysokościowe"
            };

            Composite kota;

            kota = new Composite()
            {
                LeafName = "kota01",
                LeafTitle = "Architektoniczna kota wysokościowa",
                Layer = "0",
                Linetype = "BYLAYER",
                ColorIndex = 256
            };
            kota.AddLeaf(new Composite()
            {
                LeafName = "contourLine01",
                LeafTitle = "Linia konturowa koty",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 256
            });
            kota.AddLeaf(new Composite()
            {
                LeafName = "markSign",
                LeafTitle = "Znak poziomu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 7
            });
            kota.AddLeaf(new Composite()
            {
                LeafName = "markValue",
                LeafTitle = "Wartość poziomu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 7
            });
            module.AddLeaf(kota);

            kota = new Composite()
            {
                LeafName = "kota02",
                LeafTitle = "Konstrukcyjna kota wysokościowa",
                Layer = "0",
                Linetype = "BYLAYER",
                ColorIndex = 256
            };
            kota.AddLeaf(new Composite()
            {
                LeafName = "contourLine01",
                LeafTitle = "Linia konturowa koty",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 256
            });
            kota.AddLeaf(new Composite()
            {
                LeafName = "contourFill",
                LeafTitle = "Wypełnienie grotu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 2
            });
            kota.AddLeaf(new Composite()
            {
                LeafName = "markSign",
                LeafTitle = "Znak poziomu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 7
            });
            kota.AddLeaf(new Composite()
            {
                LeafName = "markValue",
                LeafTitle = "Wartość poziomu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 7
            });
            module.AddLeaf(kota);

            kota = new Composite()
            {
                LeafName = "kota03",
                LeafTitle = "Kota wysokościowa PN-B",
                Layer = "0",
                Linetype = "BYLAYER",
                ColorIndex = 256
            };
            kota.AddLeaf(new Composite()
            {
                LeafName = "contourLine01",
                LeafTitle = "Linia konturowa koty",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 256
            });
            kota.AddLeaf(new Composite()
            {
                LeafName = "contourLine02",
                LeafTitle = "Grot koty",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 256
            });
            kota.AddLeaf(new Composite()
            {
                LeafName = "markSign",
                LeafTitle = "Znak poziomu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 7
            });
            kota.AddLeaf(new Composite()
            {
                LeafName = "markValue",
                LeafTitle = "Wartość poziomu",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 7
            });
            module.AddLeaf(kota);

            composites.Add(module);

            module = new Composite()
            {
                LeafName = "markModule",
                LeafTitle = "Inne obiekty"
            };

            kota = new Composite()
            {
                LeafName = "Dziura",
                LeafTitle = "Obiekt dziura",
                Layer = "0",
                Linetype = "BYLAYER",
                ColorIndex = 256
            };
            kota.AddLeaf(new Composite()
            {
                LeafName = "Element dziury",
                LeafTitle = "Nazwa elementu dziury",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 256
            });
            kota.AddLeaf(new Composite()
            {
                LeafName = "Drugi element dziury",
                LeafTitle = "Nazwa drugiego elementu dziury",
                Layer = "BYBLOCK",
                Linetype = "BYLAYER",
                ColorIndex = 256
            });

            module.AddLeaf(kota);

            composites.Add(module);
        }
    }
}
