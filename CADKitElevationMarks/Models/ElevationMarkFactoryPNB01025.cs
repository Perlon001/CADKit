using CADKitElevationMarks.Contracts;
using System;

namespace CADKitElevationMarks.Models
{
    public class ElevationMarkFactoryPNB01025 : ElevationMarkFactory, IElevationMarkFactoryPNB01025
    {
        public override IElevationMark Create(ElevationMarkType type, IElevationMarkConfig _config)
        {
            switch(type)
            {
                case ElevationMarkType.archMark:
                    return new ArchtecturalElevationMarkPNB01025(_config, new JigSectionMarkCADKit());
                case ElevationMarkType.constrMark:
                    return new ConstructionElevationMarkPNB01025(_config, new JigSectionMarkCADKit());
                case ElevationMarkType.planeMark:
                    return new PlaneElevationMarkPNB01025(_config, new JigSectionMarkCADKit());
                default:
                    throw new NotImplementedException($"Nie zaimplementowany typ {type.ToString()}");
            }
        }
    }
}
