using CADKit.Contracts;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;

namespace CADKitElevationMarks.Services
{
    public class MarkTypeServicePNB01025 : MarkTypeService, IMarkTypeServicePNB01025
    {
        public MarkTypeServicePNB01025(IMarkIconServicePNB01025 _iconService) : base(_iconService)
        {
            int i = 0;
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.PNB01025,
                type = MarkTypes.universal,
                markClass = typeof(ElevationMarkPNB01025),
                picture16 = iconService.GetIcon(DrawingStandards.PNB01025, MarkTypes.universal),
                picture32 = iconService.GetIcon(DrawingStandards.PNB01025, MarkTypes.universal, IconSize.medium)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.PNB01025,
                type = MarkTypes.area,
                markClass = typeof(PlaneElevationMarkPNB01025),
                picture16 = iconService.GetIcon(DrawingStandards.PNB01025, MarkTypes.area),
                picture32 = iconService.GetIcon(DrawingStandards.PNB01025, MarkTypes.area, IconSize.medium)
            });

        }
    }
}
