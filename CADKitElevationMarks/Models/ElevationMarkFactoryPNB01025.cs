using System;
using System.Collections.Generic;
using System.Drawing;
using CADKitElevationMarks.Contracts;

namespace CADKitElevationMarks.Models
{
    
    public class ElevationMarkFactoryPNB01025 : ElevationMarkFactory
    {
        public ElevationMarkFactoryPNB01025(IIconService _iconService) : base(_iconService) { }
        
        public override IEnumerable<ElevationMarkItem> GetMarkTypeList()
        {
            var result = new List<ElevationMarkItem>();
            result.Add(new ElevationMarkItem() { ElevationMarkType = typeof(PlaneElevationMarkPNB01025), Kind = MarkTypes.area, Icon = iconService.GetIcon(MarkTypes.area)});
            result.Add(new ElevationMarkItem() { ElevationMarkType = typeof(ElevationMarkPNB01025), Kind = MarkTypes.universal, Icon = iconService.GetIcon(MarkTypes.universal) });

            return result;
        }
    }
}
