using Autofac;
using CADKitCore.Util;
using CADKitElevationMarks.Contract;
using System;

namespace CADKitElevationMarks.Model
{
    public class ElevationMarkFactoryPNB01025 : ElevationMarkFactory, IElevationMarkFactoryPNB01025
    {
        public override IElevationMark GetElevationMark(ElevationMarkType type, IElevationMarkConfig config)
        {
            switch(type)
            {
                case ElevationMarkType.archMark:
                    return new ArchtecturalElevationMarkPNB01025(config);
                case ElevationMarkType.constrMark:
                    return new ConstructionElevationMarkPNB01025(config);
                case ElevationMarkType.planeMark:
                    return new PlaneElevationMarkPNB01025(config);
                default:
                    throw new NotImplementedException($"Nie zaimplementowany typ {type.ToString()}");
            }
        }
    }
}
