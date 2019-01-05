using CADKit.Contract.Services;
using CADKit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Services
{
    class LocalCompositService : ICompositeService
    {
        public SortedDictionary<string, Composite> LoadComposites()
        {
            Composite kota;
            var module = new Composite() { LeafName = "markModule", LeafTitle = "Koty wysokościowe" };

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

            var result = new SortedDictionary<string, Composite>();
            result.Add( "", module);
            return result;
        }
    }
}
