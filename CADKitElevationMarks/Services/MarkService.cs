using Autofac;
using CADKit;
using CADKit.Contracts;
using CADKit.Extensions;
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
        protected Dictionary<MarkTypes, string> markTitle = new Dictionary<MarkTypes, string>();
        protected Dictionary<DrawingStandards, string> markStandard = new Dictionary<DrawingStandards, string>();
        protected IList<MarkDTO> markCollection = new List<MarkDTO>();

        public MarkService(IMarkIconService _iconService)
        {
            iconService = _iconService;
            markTitle.Add(MarkTypes.area, "Rzędna obszaru");
            markTitle.Add(MarkTypes.universal, "Kota wysokościowa");
            markTitle.Add(MarkTypes.construction, "Kota wysokościowa konstrukcji");
            markTitle.Add(MarkTypes.finish, "Kota wysokościowa wykończenia");
            markTitle.Add(MarkTypes.strainedwater, "Napięte zwierciadło wody");
            markTitle.Add(MarkTypes.water, "Swobodne zwierciadło wody");

            markStandard.Add(DrawingStandards.PNB01025, "PNB01025");
            markStandard.Add(DrawingStandards.Std01, "Std01");
            markStandard.Add(DrawingStandards.Std02, "Std02");

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

        public IEnumerable<MarkButtonDTO> GetMarkButtons()
        {
            return markCollection.Select(y => new MarkButtonDTO() { id = y.id, name = GetMarkDescription(y.id), picture = y.picture32 });
        }
        
        public IEnumerable<MarkDTO> GetMarks()
        {
            return markCollection;
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
            return markStandard[GetMarkDTO(_markNumber).standard] + " - " + markTitle[GetMarkDTO(_markNumber).type];
        }

        public string GetMarkDescription(DrawingStandards _standard, MarkTypes _type)
        {
            return markTitle[GetMarkDTO(_standard, _type).type];
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

        public ICollection<IComponent> GetComponents()
        {
            var result = new List<IComponent>();
            using(var scope = DI.Container.BeginLifetimeScope())
            {
                foreach(var m in markCollection)
                {
                    var mark = (Mark)scope.Resolve(m.markType);
                    mark.Image = m.picture16;
                    result.Add(mark);
                }
            }

            return result;
        }
    }
}
