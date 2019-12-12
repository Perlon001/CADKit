// using Autofac;
using System;

using CADKit;
using CADKitElevationMarks.Models;
using CADProxy.Runtime;

namespace CADKitElevationMarks
{
    public class ElevationMarkCommands
    {
        [CommandMethod("CK_KOTA_ARCH")]
        public void ElevationMarkArch()
        {
            var factory = GetElevationMarkFactory();
            factory.ArchitecturalElevationMark().Create();
        }

        [CommandMethod("CK_KOTA_KONSTR")]
        public void ElevationMarkConstr()
        {
            var factory = GetElevationMarkFactory();
            factory.ConstructionElevationMark().Create();
        }

        [CommandMethod("CK_KOTA_POZIOM")]
        public void ElevationMarkPlate()
        {
            var factory = GetElevationMarkFactory();
            factory.PlaneElevationMark().Create();
        }

        private ElevationMarkFactory GetElevationMarkFactory()
        {
            var factoryName = "ElevationMarkFactory" + AppSettings.Instance.DrawingStandard.ToString();
            return Activator.CreateInstance(Type.GetType("CADKitElevationMarks.Models." + factoryName, true)) as ElevationMarkFactory;

            //switch (AppSettings.Instance.DrawingStandard)
            //{
            //    case DrawingStandards.PNB01025:
            //        return factory = new ElevationMarkFactoryPNB01025();
            //    case DrawingStandards.CADKIT:
            //        return factory = new ElevationMarkFactoryCADKit();
            //    default:
            //        throw new NotImplementedException($"Brak implemetacji standardu {AppSettings.Instance.DrawingStandard.ToString()}");
            //}

            // Alternative code using Autofac Dependecy Injection Container 
            //using (ILifetimeScope scope = DI.Container.BeginLifetimeScope())
            //{
            //    switch (AppSettings.Instance.DrawingStandard)
            //    {
            //        case DrawingStandards.PNB01025:
            //            return factory = scope.Resolve<IElevationMarkFactoryPNB01025>();
            //        case DrawingStandards.CADKIT:
            //            return factory = scope.Resolve<IElevationMarkFactoryCADKit>();
            //        default:
            //            throw new NotImplementedException($"Brak implemetacji standardu {AppSettings.Instance.DrawingStandard.ToString()}");
            //    }
            //}
        }
    }
}
