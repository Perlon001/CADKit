using CADKit.Contracts;
using CADKitElevationMarks.Contract.Services;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CADKitElevationMarks.Services
{
    public class MarkService : IMarkService
    {
        protected readonly IMarkIconService iconService;
        protected struct markItem
        {
            public int id;
            public MarkTypes type;
            public Type markClass;
            public Bitmap picture16;
            public Bitmap picture32;
        }
        protected Dictionary<MarkTypes, string> markTypes = new Dictionary<MarkTypes, string>();
        protected IList<markItem> markCollection = new List<markItem>();

        public MarkService(IMarkIconService _iconService)
        {
            iconService = _iconService;
            markTypes.Add(MarkTypes.area, "Rzędna obszaru");
            markTypes.Add(MarkTypes.universal, "Kota wysokościowa");
            markTypes.Add(MarkTypes.construction, "Kota wysokościowa konstrukcji");
            markTypes.Add(MarkTypes.finish, "Kota wysokościowa wykończenia");
            markTypes.Add(MarkTypes.strainedwater, "Napięte zwierciadło wody");
            markTypes.Add(MarkTypes.water, "Swobodne zwierciadło wody");

            int i = 0;
            markCollection.Add(new markItem()
            {
                id = i++,
                type = MarkTypes.universal,
                markClass = typeof(MarkPNB01025),
                picture16 = iconService.GetIcon(DrawingStandards.PNB01025, MarkTypes.universal),
                picture32 = iconService.GetIcon(DrawingStandards.PNB01025, MarkTypes.universal, IconSize.medium)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                type = MarkTypes.area,
                markClass = typeof(PlaneMarkPNB01025),
                picture16 = iconService.GetIcon(DrawingStandards.PNB01025, MarkTypes.area),
                picture32 = iconService.GetIcon(DrawingStandards.PNB01025, MarkTypes.area, IconSize.medium)
            });
        }

        public string GetMarkName(int _markNumber)
        {
            var item = markCollection.FirstOrDefault(x => x.id == _markNumber);
            if (item.Equals(default(markItem)))
            {
                throw new NotSupportedException("Brak koty wysokosciowej o numerze " + _markNumber.ToString());
            }
            else
            {
                return markTypes[item.type];
            }
        }

        public string GetMarkName(MarkTypes _type)
        {
            var item = markCollection.FirstOrDefault(x => x.markClass.Equals(_type));
            if (item.Equals(default(markItem)))
            {
                throw new NotSupportedException("Brak koty wysokosciowej " + _type.ToString());
            }
            else
            {
                return markTypes[item.type];
            }
        }

        public IList<MarkButtonDTO> GetMarks()
        {
            return markCollection
                .Select(y => { return new MarkButtonDTO() { id = y.id, name = this.GetMarkName(y.id), picture = y.picture32 }; })
                .ToList();
        }

        public Type GetMarkType(int markNumber)
        {
            var item = markCollection.FirstOrDefault(x => x.id == markNumber);
            if (item.Equals(default(markItem)))
            {
                throw new NotSupportedException("Brak koty wysokosciowej o numerze " + markNumber.ToString());
            }
            else
            {
                return item.markClass;
            }
        }

        IEnumerable<IMark> IMarkService.GetMarks()
        {
            throw new NotImplementedException();
        }

        IMark IMarkService.GetMrk(DrawingStandards standard, MarkTypes type)
        {
            throw new NotImplementedException();
        }
    }
}
