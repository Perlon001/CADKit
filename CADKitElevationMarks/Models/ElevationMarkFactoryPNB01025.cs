using System;

using CADKitElevationMarks.Contracts;

namespace CADKitElevationMarks.Models
{
    public class ElevationMarkFactoryPNB01025 : ElevationMarkFactory
    {
        public IElevationMark CreateElevationMarkSimpleFactory(ElevationMarkType _type, ElevationValue _value, IElevationMarkConfig _config)
        {
            switch (_type)
            {
                case ElevationMarkType.archMark:
                    return new ArchitecturalElevationMarkPNB01025(_value, _config);
                case ElevationMarkType.constrMark:
                    return new ConstructionElevationMarkPNB01025(_value, _config);
                case ElevationMarkType.planeMark:
                    return new PlaneElevationMarkPNB01025(_value, _config);
                default:
                    throw new NotImplementedException($"Nie zaimplementowany typ {_type.ToString()}");
            }
        }
        public override IElevationMark CreateElevationMarkFactory(ElevationMarkType _type, ElevationValue _value, IElevationMarkConfig _config)
        {
            return CreateElevationMarkSimpleFactory(_type, _value, _config);
        }
        //public override IElevationMark CreateArchitecturalElevationMark(ElevationValue value, IElevationMarkConfig config)
        //{
        //    return new ArchitecturalElevationMarkPNB01025(value,config);
        //}

        //public override IElevationMark CreateConstructionElevationMark(ElevationValue value, IElevationMarkConfig config)
        //{
        //    return new ConstructionElevationMarkPNB01025(value, config);
        //}

        //public override IElevationMark CreatePlaneElevationMark(ElevationValue value, IElevationMarkConfig config)
        //{
        //    return new PlaneElevationMarkPNB01025(value, config);
        //}
    }
}
