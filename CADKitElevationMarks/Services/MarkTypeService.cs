using CADKit.Models;
using CADKit.Utils;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Services
{
    public class MarkTypeService : IMarkTypeStandards
    {
        public struct markItem
        {
            public string name;
            public DrawingStandards standard;
            public Type markClass;
            public Bitmap picture;
        }
        private IList<markItem> markCollection;
        public MarkTypeService()
        {
            markCollection = new List<markItem>();
            markCollection.Add(new markItem()
            {
                name = "Kota wysokościowa",
                standard = DrawingStandards.PNB01025,
                markClass = typeof(ElevationMarkPNB01025),
            });
            markCollection.Add(new markItem()
            {
                name = "Rzędna obszaru",
                standard = DrawingStandards.PNB01025,
                markClass = typeof(PlaneElevationMarkPNB01025)
            });
            markCollection.Add(new markItem()
            {
                name = "Kota wysokościowa wykończenia",
                standard = DrawingStandards.CADKit,
                markClass = typeof(FinishElevationMarkCADKit)
            });
            markCollection.Add(new markItem()
            {
                name = "Kota wysokościowa konstrukcji",
                standard = DrawingStandards.CADKit,
                markClass = typeof(ConstructionElevationMarkCADKit)
            });
            markCollection.Add(new markItem()
            {
                name = "Rzędna obszaru",
                standard = DrawingStandards.CADKit,
                markClass = typeof(PlaneElevationMarkCADKit)
            });
        }

        public IList<MarkButtonDTO> GetMarksList()
        {
            var result = new List<MarkButtonDTO>();
            int i = 0;

            return markCollection
                .Select(y => { return new MarkButtonDTO() { name = y.name, number = i++, picture = y.picture }; })
                .ToList();
        }

        public IList<MarkButtonDTO> GetMarksList(DrawingStandards standard)
        {
            int i = 0;
            return markCollection
                .Where(x => x.standard == standard)
                .Select(y => { return new MarkButtonDTO() { name = y.name, number = i++, picture = y.picture }; })
                .ToList();
        }

        public Dictionary<string, DrawingStandards> GetStandards()
        {
            return EnumsUtil.GetEnumDictionary<DrawingStandards>();
        }

        public Dictionary<string, MarkTypes> GetTypes()
        {
            return EnumsUtil.GetEnumDictionary<MarkTypes>();
        }

        IList<MarkButtonDTO> IMarkTypeStandards.GetMarksList(DrawingStandards standard)
        {
            throw new NotImplementedException();
        }
    }
}
