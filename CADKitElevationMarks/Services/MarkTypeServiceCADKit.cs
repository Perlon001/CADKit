using Autofac;
using CADKit;
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
            using(var scope = DI.Container.BeginLifetimeScope())
            {
                iconService = scope.Resolve<IIconServiceCADKit>();
                int i = 0;
                markCollection.Add(new markItem()
                {
                    id = i++,
                    standard = DrawingStandards.CADKit,
                    type = MarkTypes.finish,
                    markClass = typeof(FinishElevationMarkCADKit),
                    picture16 = iconService.GetIcon(MarkTypes.finish),
                    picture32 = iconService.GetIcon(MarkTypes.finish, IconSize.medium)
                });
                markCollection.Add(new markItem()
                {
                    id = i++,
                    standard = DrawingStandards.CADKit,
                    type = MarkTypes.construction,
                    markClass = typeof(ConstructionElevationMarkCADKit),
                    picture16 = iconService.GetIcon(MarkTypes.construction),
                    picture32 = iconService.GetIcon(MarkTypes.construction, IconSize.medium)
                });
                markCollection.Add(new markItem()
                {
                    id = i++,
                    standard = DrawingStandards.CADKit,
                    type = MarkTypes.area,
                    markClass = typeof(PlaneElevationMarkCADKit),
                    picture16 = iconService.GetIcon(MarkTypes.area),
                    picture32 = iconService.GetIcon(MarkTypes.area, IconSize.medium)
                });
            }
        }
    }
}
