using CADKit.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CADKitElevationMarks.Services
{
    public class MarkTypeService : IMarkTypeService
    {
        struct markItem
        {
            public int id;
            public DrawingStandards standard;
            public MarkTypes type;
            public Type markClass;
            public Bitmap picture16;
            public Bitmap picture32;
        }

        private Dictionary<MarkTypes, string> markTypes = new Dictionary<MarkTypes, string>();

        private IList<markItem> markCollection = new List<markItem>();

        public MarkTypeService()
        {
            markTypes.Add(MarkTypes.area, "Rzędna obszaru");
            markTypes.Add(MarkTypes.construction, "Kota wysokościowa konstrukcji");
            markTypes.Add(MarkTypes.finish, "Kota wysokościowa wykończenia");
            markTypes.Add(MarkTypes.strainedwater, "Napięte zwierciadło wody");
            markTypes.Add(MarkTypes.universal, "Kota wysokościowa");
            markTypes.Add(MarkTypes.water, "Swobodne zwierciadło wody");

            int i = 0;
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.PNB01025,
                type=MarkTypes.universal,
                markClass = typeof(ElevationMarkPNB01025),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.PNB01025,
                type=MarkTypes.area,
                markClass = typeof(PlaneElevationMarkPNB01025),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.CADKit,
                type=MarkTypes.finish,
                markClass = typeof(FinishElevationMarkCADKit),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.CADKit,
                type=MarkTypes.construction,
                markClass = typeof(ConstructionElevationMarkCADKit),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.CADKit,
                type=MarkTypes.area,
                markClass = typeof(PlaneElevationMarkCADKit),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
        }

        public IList<MarkButtonDTO> GetMarks()
        {
            return markCollection
                .Select(y => { return new MarkButtonDTO() { id = y.id, name = this.GetMarkName(y.id), picture = y.picture32 }; })
                .ToList();
        }

        public IList<MarkButtonDTO> GetMarks(DrawingStandards standard)
        {
            return markCollection
                .Where(x => x.standard == standard)
                .Select(y => { return new MarkButtonDTO() { id = y.id, name = this.GetMarkName(y.id), picture = y.picture32 }; })
                .ToList();
        }

        public IList<MarkButtonDTO> GetMarks(DrawingStandards standard, MarkTypes[] types)
        {
            throw new NotImplementedException();
        }

        public Type GetMarkType(int markNumber)
        {
            return markCollection
                .First(x => x.id == markNumber).markClass;
        }

        public IList<MarkButtonDTO> GetMarks(MarkTypes[] types)
        {
            throw new NotImplementedException();
        }

        public string GetMarkName(int markNumber)
        {
            var item = markCollection.FirstOrDefault(x => x.id == markNumber);
            if(item.Equals(default(markItem)))
            {
                return "";
            }
            else
            {
                return markTypes[item.type];
            }
        }

        public string GetMarkName(DrawingStandards _standard, MarkTypes _type)
        {
            var item = markCollection.FirstOrDefault(x => x.standard == _standard && x.type == _type);
            if (item.Equals(default(markItem)))
            {
                return "";
            }
            else
            {
                return markTypes[item.type];
            }
        }
    }
}
