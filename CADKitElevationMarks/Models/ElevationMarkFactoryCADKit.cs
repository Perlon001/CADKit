using System;

using CADKitElevationMarks.Contracts;

namespace CADKitElevationMarks.Models
{
    public class ElevationMarkFactoryCADKit : ElevationMarkFactory
    {
        //public override IElevationMark CreateElevationMarkFactory(ElevationMarkType _type, ElevationValue _value, IElevationMarkConfig _config)
        //{
        //    switch (_type)
        //    {
        //        case ElevationMarkType.archMark:
        //            return new ArchitecturalElevationMarkCADKit(_value, _config);
        //        case ElevationMarkType.constrMark:
        //            return new ConstructionElevationMarkCADKit(_value, _config);
        //        case ElevationMarkType.planeMark:
        //            return new PlaneElevationMarkCADKit(_value, _config);
        //        default:
        //            throw new NotImplementedException($"Nie zaimplementowany typ {_type.ToString()}");
        //    }
        //}
        public override IElevationMark CreateArchitecturalElevationMark(ElevationValue value, IElevationMarkConfig config)
        {
            return new ArchitecturalElevationMarkCADKit(value, config);
        }

        public override IElevationMark CreateConstructionElevationMark(ElevationValue value, IElevationMarkConfig config)
        {
            return new ConstructionElevationMarkCADKit(value, config);
        }

        public override IElevationMark CreatePlaneElevationMark(ElevationValue value, IElevationMarkConfig config)
        {
            return new PlaneElevationMarkCADKit(value, config);
        }
    }
}
