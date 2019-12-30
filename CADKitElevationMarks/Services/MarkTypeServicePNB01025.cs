using CADKit.Models;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class MarkTypeServicePNB01025 : MarkTypeService, IMarkTypeServicePNB01025
    {
        public MarkTypeServicePNB01025() : base()
        {
            int i = 0;
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.PNB01025,
                type = MarkTypes.universal,
                markClass = typeof(ElevationMarkPNB01025),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.PNB01025,
                type = MarkTypes.area,
                markClass = typeof(PlaneElevationMarkPNB01025),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
        }
    }
}
