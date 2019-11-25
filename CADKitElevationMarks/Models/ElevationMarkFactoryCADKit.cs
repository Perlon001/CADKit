using CADKitElevationMarks.Contracts;
using System;

namespace CADKitElevationMarks.Models
{
    public class ElevationMarkFactoryCADKit : ElevationMarkFactory, IElevationMarkFactoryCADKit
    {
        public override IElevationMark Create(ElevationMarkType type, IElevationMarkConfig _config)
        {
            switch (type)
            {
                case ElevationMarkType.archMark:
                    return new ArchitecturalElevationMarkCADKit(_config, new JigSectionMarkCADKit());
                case ElevationMarkType.constrMark:
                    return new ConstructionElevationMarkCADKit(_config, new JigSectionMarkCADKit());
                case ElevationMarkType.planeMark:
                    return new PlaneElevationMarkCADKit(_config, new JigSectionMarkCADKit());
                default:
                    throw new NotImplementedException($"Nie zaimplementowany typ {type.ToString()}");
            }
        }
    }
}
