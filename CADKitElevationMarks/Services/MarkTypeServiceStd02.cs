using CADKitBasic.Models;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;

namespace CADKitElevationMarks.Services
{
    public class MarkTypeServiceStd02 : MarkTypeService, IMarkTypeServiceStd02
    {
        public MarkTypeServiceStd02(IIconServiceStd02 _iconService) : base(_iconService)
        {
            int i = 0;
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.Std02,
                type = MarkTypes.finish,
                markClass = typeof(FinishElevationMarkStd01),
                picture16 = iconService.GetIcon(MarkTypes.finish),
                picture32 = iconService.GetIcon(MarkTypes.finish, IconSize.medium)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.Std02,
                type = MarkTypes.construction,
                markClass = typeof(ConstructionElevationMarkStd01),
                picture16 = iconService.GetIcon(MarkTypes.construction),
                picture32 = iconService.GetIcon(MarkTypes.construction, IconSize.medium)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.Std02,
                type = MarkTypes.area,
                markClass = typeof(PlaneElevationMarkStd01),
                picture16 = iconService.GetIcon(MarkTypes.area),
                picture32 = iconService.GetIcon(MarkTypes.area, IconSize.medium)
            });
        }
    }
}
