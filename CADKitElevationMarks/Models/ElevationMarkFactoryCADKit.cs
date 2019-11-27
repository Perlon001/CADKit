using CADKitElevationMarks.Contracts;
using System;

namespace CADKitElevationMarks.Models
{
    public class ElevationMarkFactoryCADKit : ElevationMarkFactory, IElevationMarkFactoryCADKit
    {
        public override IElevationMark GetElevationMark(ElevationMarkType type, IElevationMarkConfig _config)
        {
            switch (type)
            {
                case ElevationMarkType.archMark:
                    return new ArchitecturalElevationMarkCADKit(_config);
                case ElevationMarkType.constrMark:
                    return new ConstructionElevationMarkCADKit(_config);
                case ElevationMarkType.planeMark:
                    return new PlaneElevationMarkCADKit(_config);
                default:
                    throw new NotImplementedException($"Nie zaimplementowany typ {type.ToString()}");
            }
        }
    }
}
