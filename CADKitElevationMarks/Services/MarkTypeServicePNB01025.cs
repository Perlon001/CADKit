using CADKitBasic.Models;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;

namespace CADKitElevationMarks.Services
{
    public class MarkTypeServicePNB01025 : MarkTypeService, IMarkTypeServicePNB01025
    {
        public MarkTypeServicePNB01025(IIconServicePNB01025 _iconService) : base(_iconService)
        {
            int i = 0;
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.PNB01025,
                type = MarkTypes.universal,
                markClass = typeof(ElevationMarkPNB01025),
                picture16 = iconService.GetIcon(MarkTypes.universal),
                picture32 = iconService.GetIcon(MarkTypes.universal, IconSize.medium)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.PNB01025,
                type = MarkTypes.area,
                markClass = typeof(PlaneElevationMarkPNB01025),
                picture16 = iconService.GetIcon(MarkTypes.area),
                picture32 = iconService.GetIcon(MarkTypes.area, IconSize.medium)
            });

        }
    }
}
