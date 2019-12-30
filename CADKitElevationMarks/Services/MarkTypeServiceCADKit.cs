using CADKit.Models;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Contracts.Services;
using CADKitElevationMarks.Models;
using System.Drawing;

namespace CADKitElevationMarks.Services
{
    public class MarkTypeServiceCADKit : MarkTypeService, IMarkTypeServiceCADKit
    {
        public MarkTypeServiceCADKit() : base()
        {
            int i = 0;
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.CADKit,
                type = MarkTypes.finish,
                markClass = typeof(FinishElevationMarkCADKit),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.CADKit,
                type = MarkTypes.construction,
                markClass = typeof(ConstructionElevationMarkCADKit),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
            markCollection.Add(new markItem()
            {
                id = i++,
                standard = DrawingStandards.CADKit,
                type = MarkTypes.area,
                markClass = typeof(PlaneElevationMarkCADKit),
                picture16 = new Bitmap(16, 16),
                picture32 = new Bitmap(32, 32)
            });
        }
    }
}
