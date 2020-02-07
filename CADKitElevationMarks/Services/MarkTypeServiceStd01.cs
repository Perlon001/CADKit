using CADKit.Contracts;
using CADKitBasic.Models;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;

namespace CADKitElevationMarks.Services
{
    public class MarkTypeServiceStd01 : MarkTypeService, IMarkTypeServiceStd01
    {
        public MarkTypeServiceStd01(IMarkIconServiceStd01 _iconService) : base(_iconService)
        {
            int i = 0;
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.Std01,
                type = MarkTypes.finish,
                markClass = typeof(FinishElevationMarkStd01),
                picture16 = iconService.GetIcon(DrawingStandards.Std01, MarkTypes.finish),
                picture32 = iconService.GetIcon(DrawingStandards.Std01, MarkTypes.finish, IconSize.medium)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.Std01,
                type = MarkTypes.construction,
                markClass = typeof(ConstructionElevationMarkStd01),
                picture16 = iconService.GetIcon(DrawingStandards.Std01, MarkTypes.construction),
                picture32 = iconService.GetIcon(DrawingStandards.Std01, MarkTypes.construction, IconSize.medium)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.Std01,
                type = MarkTypes.area,
                markClass = typeof(PlaneElevationMarkStd01),
                picture16 = iconService.GetIcon(DrawingStandards.Std01, MarkTypes.area),
                picture32 = iconService.GetIcon(DrawingStandards.Std01, MarkTypes.area, IconSize.medium)
            });
        }
    }
}
