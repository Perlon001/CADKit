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
        //            return new ArchitecturalElevationMarkCADKit();
        //        case ElevationMarkType.constrMark:
        //            return new ConstructionElevationMarkCADKit();
        //        case ElevationMarkType.planeMark:
        //            return new PlaneElevationMarkCADKit();
        //        default:
        //            throw new NotImplementedException($"Nie zaimplementowany typ {_type.ToString()}");
        //    }
        //}

        public override ElevationMark ArchitecturalElevationMark()
        {
            return new ArchitecturalElevationMarkCADKit();
        }

        public override ElevationMark ConstructionElevationMark()
        {
            return new ConstructionElevationMarkCADKit();
        }

        public override ElevationMark PlaneElevationMark()
        {
            return new PlaneElevationMarkCADKit();
        }
    }
}
