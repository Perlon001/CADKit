// using Autofac;
using System;
using System.Linq;

using CADKit;
using CADKitElevationMarks.Contracts;
using CADKitElevationMarks.Models;
using CADProxy;
using CADProxy.Runtime;

namespace CADKitElevationMarks
{
    public class ElevationMarkCommands
    {
        //[CommandMethod("CK_KOTA_ARCH")]
        //public void ElevationMarkArch()
        //{
        //    var k = new ElevationMarkPNB01025();
        //    k.Create();

        //    // CreateMark(MarkTypes.finish);
        //}

        //[CommandMethod("CK_KOTA_KONSTR")]
        //public void ElevationMarkConstr()
        //{
        //    var factory = GetElevationMarkFactory();
        //    factory.ConstructionElevationMark().Create();
        //}

        //[CommandMethod("CK_KOTA_POZIOM")]
        //public void ElevationMarkPlate()
        //{
        //    var factory = GetElevationMarkFactory();
        //    factory.PlaneElevationMark().Create();
        //}

        //private void CreateMark(MarkTypes type)
        //{
        //    var factory = GetElevationMarkFactory();
        //    var item = factory.GetMarkTypeList().FirstOrDefault(m => m.Kind == type);
        //    if (item.ElevationMarkType != null)
        //    {
        //        (Activator.CreateInstance(item.ElevationMarkType) as ElevationMark).Create();
        //    }
        //    else
        //    {
        //        ProxyCAD.WriteMessage("\nNie zdefiniowany typ koty wysokościowej");
        //    }
        //}

        //private IIconServiceFactory GetIconServiceFactory()
        //{
        //    var iconServiceFactory = "IconServiceFactory" + AppSettings.Instance.DrawingStandard.ToString();
        //    return Activator.CreateInstance(Type.GetType("CADKitElevationMarks.Services." + iconServiceFactory, true)) as IIconServiceFactory;
        //}

        //private ElevationMarkFactory GetElevationMarkFactory()
        //{
        //    var factoryName = "ElevationMarkFactory" + AppSettings.Instance.DrawingStandard.ToString();
        //    var iconServiceFactory = GetIconServiceFactory();
        //    return Activator.CreateInstance(Type.GetType("CADKitElevationMarks.Models." + factoryName, true), iconServiceFactory.CreateService()) as ElevationMarkFactory;

        //    //switch (AppSettings.Instance.DrawingStandard)
        //    //{
        //    //    case DrawingStandards.PNB01025:
        //    //        return factory = new ElevationMarkFactoryPNB01025();
        //    //    case DrawingStandards.CADKIT:
        //    //        return factory = new ElevationMarkFactoryCADKit();
        //    //    default:
        //    //        throw new NotImplementedException($"Brak implemetacji standardu {AppSettings.Instance.DrawingStandard.ToString()}");
        //    //}

        //    // Alternative code using Autofac Dependecy Injection Container 
        //    //using (ILifetimeScope scope = DI.Container.BeginLifetimeScope())
        //    //{
        //    //    switch (AppSettings.Instance.DrawingStandard)
        //    //    {
        //    //        case DrawingStandards.PNB01025:
        //    //            return factory = scope.Resolve<IElevationMarkFactoryPNB01025>();
        //    //        case DrawingStandards.CADKIT:
        //    //            return factory = scope.Resolve<IElevationMarkFactoryCADKit>();
        //    //        default:
        //    //            throw new NotImplementedException($"Brak implemetacji standardu {AppSettings.Instance.DrawingStandard.ToString()}");
        //    //    }
        //    //}
        //}
    }
}
