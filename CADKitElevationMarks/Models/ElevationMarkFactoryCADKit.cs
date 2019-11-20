using CADKitElevationMarks.Contracts;
using System;

namespace CADKitElevationMarks.Models
{
    public class ElevationMarkFactoryCADKit : ElevationMarkFactory, IElevationMarkFactoryCADKit
    {
        public override IElevationMark ElevationMark(ElevationMarkType type, IElevationMarkConfig config)
        {
            switch (type)
            {
                case ElevationMarkType.archMark:
                    return new ArchitecturalElevationMarkCADKit(config);
                case ElevationMarkType.constrMark:
                    return new ConstructionElevationMarkCADKit(config);
                case ElevationMarkType.planeMark:
                    return new PlaneElevationMarkCADKit(config);
                default:
                    throw new NotImplementedException($"Nie zaimplementowany typ {type.ToString()}");
            }
        }
    }
}
