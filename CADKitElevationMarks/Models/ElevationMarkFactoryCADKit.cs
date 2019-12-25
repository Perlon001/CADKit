using System;
using System.Collections.Generic;
using CADKitElevationMarks.Contracts;

namespace CADKitElevationMarks.Models
{
    public class ElevationMarkFactoryCADKit : ElevationMarkFactory
    {
        public ElevationMarkFactoryCADKit(IIconService _iconService) : base(_iconService) { }
        
        public override IEnumerable<ElevationMarkItem> GetMarkTypeList()
        {
            var result = new List<ElevationMarkItem>();
            result.Add(new ElevationMarkItem() { ElevationMarkType = typeof(PlaneElevationMarkCADKit), Kind = MarkTypes.area, Icon=iconService.GetIcon(MarkTypes.area) });
            result.Add(new ElevationMarkItem() { ElevationMarkType = typeof(FinishElevationMarkCADKit), Kind = MarkTypes.finish, Icon = iconService.GetIcon(MarkTypes.finish) });
            result.Add(new ElevationMarkItem() { ElevationMarkType = typeof(ConstructionElevationMarkCADKit), Kind = MarkTypes.construction, Icon = iconService.GetIcon(MarkTypes.construction) });

            return result;
        }

    }
}
