using System;

using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Modelsm;

namespace CADKitElevationMarks.Models
{
    public class ElevationMarkFactoryPNB01025 : ElevationMarkFactory
    {
        //public IElevationMark CreateElevationMarkSimpleFactory(ElevationMarkType _type, ElevationValue _value, IElevationMarkConfig _config)
        //{
        //    switch (_type)
        //    {
        //        case ElevationMarkType.archMark:
        //            return new ArchitecturalElevationMarkPNB01025();
        //        case ElevationMarkType.constrMark:
        //            return new ConstructionElevationMarkPNB01025();
        //        case ElevationMarkType.planeMark:
        //            return new PlaneElevationMarkPNB01025();
        //        default:
        //            throw new NotImplementedException($"Nie zaimplementowany typ {_type.ToString()}");
        //    }
        //}
        //public override IElevationMark CreateElevationMarkFactory(ElevationMarkType _type, ElevationValue _value, IElevationMarkConfig _config)
        //{
        //    return CreateElevationMarkSimpleFactory(_type, _value, _config);
        //}

        public override ElevationMark ArchitecturalElevationMark()
        {
            return new ArchitecturalElevationMarkPNB01025();
        }

        public override ElevationMark ConstructionElevationMark()
        {
            return new ConstructionElevationMarkPNB01025();
        }

        public override ElevationMark PlaneElevationMark()
        {
            return new PlaneElevationMarkPNB01025();
        }
    }
}
