using CADKit.Contracts;
using CADKitElevationMarks.Contract.Services;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.DTO;
using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CADKitElevationMarks.Services
{
    public class MarkService : IMarkService
    {
        protected readonly IMarkIconService iconService;
        protected Dictionary<MarkTypes, string> markTypes = new Dictionary<MarkTypes, string>();
        protected IList<MarkDTO> markCollection = new List<MarkDTO>();

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
            markCollection.Add(new MarkDTO()
            {
                id = i++,
                standard = DrawingStandards.PNB01025,
                type = MarkTypes.universal,
                markType = typeof(MarkPNB01025),
                picture16 = iconService.GetIcon(DrawingStandards.PNB01025, MarkTypes.universal),
                picture32 = iconService.GetIcon(DrawingStandards.PNB01025, MarkTypes.universal, IconSize.medium)
            });
            markCollection.Add(new MarkDTO()
            {
                id = i++,
                standard = DrawingStandards.PNB01025,
                type = MarkTypes.area,
                markType = typeof(PlaneMarkPNB01025),
                picture16 = iconService.GetIcon(DrawingStandards.PNB01025, MarkTypes.area),
                picture32 = iconService.GetIcon(DrawingStandards.PNB01025, MarkTypes.area, IconSize.medium)
            });
            markCollection.Add(new MarkDTO()
            {
                id = i++,
                standard = DrawingStandards.Std01,
                type = MarkTypes.finish,
                markType = typeof(FinishMarkStd01),
                picture16 = iconService.GetIcon(DrawingStandards.Std01, MarkTypes.finish),
                picture32 = iconService.GetIcon(DrawingStandards.Std01, MarkTypes.finish, IconSize.medium)
            });
            markCollection.Add(new MarkDTO()
            {
                id = i++,
                standard = DrawingStandards.Std01,
                type = MarkTypes.construction,
                markType = typeof(ConstructionMarkStd01),
                picture16 = iconService.GetIcon(DrawingStandards.Std01, MarkTypes.construction),
                picture32 = iconService.GetIcon(DrawingStandards.Std01, MarkTypes.construction, IconSize.medium)
            });
            markCollection.Add(new MarkDTO()
            {
                id = i++,
                standard = DrawingStandards.Std01,
                type = MarkTypes.area,
                markType = typeof(PlaneMarkStd01),
                picture16 = iconService.GetIcon(DrawingStandards.Std01, MarkTypes.area),
                picture32 = iconService.GetIcon(DrawingStandards.Std01, MarkTypes.area, IconSize.medium)
            });
            markCollection.Add(new MarkDTO()
            {
                id = i++,
                standard = DrawingStandards.Std02,
                type = MarkTypes.finish,
                markType = typeof(FinishMarkStd02),
                picture16 = iconService.GetIcon(DrawingStandards.Std02, MarkTypes.finish),
                picture32 = iconService.GetIcon(DrawingStandards.Std02, MarkTypes.finish, IconSize.medium)
            });
            markCollection.Add(new MarkDTO()
            {
                id = i++,
                standard = DrawingStandards.Std02,
                type = MarkTypes.construction,
                markType = typeof(ConstructionMarkStd02),
                picture16 = iconService.GetIcon(DrawingStandards.Std02, MarkTypes.construction),
                picture32 = iconService.GetIcon(DrawingStandards.Std02, MarkTypes.construction, IconSize.medium)
            });
        }

        public IEnumerable<MarkButtonDTO> GetMarks()
        {
            return markCollection.Select(y => new MarkButtonDTO() { id = y.id, name = GetMarkDescription(y.id), picture = y.picture32 });
        }

        public MarkButtonDTO GetMarkButton(DrawingStandards _standard, MarkTypes _type)
        {
            var mark = markCollection.FirstOrDefault(x => x.standard == _standard && x.type == _type);
            return new MarkButtonDTO() { id = mark.id, name = GetMarkDescription(mark.id), picture = mark.picture32 };
        }

        public MarkDTO GetMark(int _markNumber)
        {
            return GetMarkDTO(_markNumber);
        }

        public string GetMarkDescription(int _markNumber)
        {
            return markTypes[GetMarkDTO(_markNumber).type];
        }

        public string GetMarkDescription(DrawingStandards _standard, MarkTypes _type)
        {
            return markTypes[GetMarkDTO(_standard, _type).type];
        }

        public Type GetMarkType(int markNumber)
        {
            var item = markCollection.FirstOrDefault(x => x.id == markNumber);
            if (item.Equals(default(MarkDTO)))
            {
                throw new Exception("Brak koty wysokosciowej o numerze " + markNumber.ToString());
            }
            else
            {
                return item.markType;
            }
        }

        private MarkDTO GetMarkDTO(int _markNumber)
        {
            var item = markCollection.FirstOrDefault(x => x.id == _markNumber);
            if (item.Equals(default(MarkDTO)))
            {
                throw new Exception("Brak koty wysokosciowej o numerze " + _markNumber.ToString());
            }
            else
            {
                return item;
            }
        }

        private MarkDTO GetMarkDTO(DrawingStandards _standard, MarkTypes _type)
        {
            var item = markCollection.FirstOrDefault(x => x.standard.Equals(_standard) && x.type.Equals(_type));
            if (item.Equals(default(MarkDTO)))
            {
                throw new Exception("Brak koty wysokościowej " + _type.ToString());
            }
            else
            {
                return item;
            }
        }

        public string GetMarkName(int _markNumber)
        {
            return GetMarkDTO(_markNumber).type.ToString();
        }
    }
}
