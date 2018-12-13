using Autofac;
using CADKitCore.Settings;
using CADKitCore.Util;
using CADKitElevationMarks.Contract;
using CADKitElevationMarks.Model;
using System;
using System.Reflection;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Colors;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Runtime;

namespace CADKitElevationMarks
{
    public class Commands
    {
        [CommandMethod("CK_KOTA_ARCH")]
        public void ElevationMarkArch()
        {
            IElevationMark mark = GetFactory(AppSettings.Instance.DrawingStandard).GetElevationMark(ElevationMarkType.archMark);
            mark.Create();

            //object acObject = Application.ZcadApplication;
            //acObject.GetType().InvokeMember("ZoomExtents", BindingFlags.InvokeMethod, null, acObject, null);
        }

        [CommandMethod("CK_KOTA_KONSTR")]
        public void ElevationMarkConstr()
        {
            IElevationMark mark = GetFactory(AppSettings.Instance.DrawingStandard).GetElevationMark(ElevationMarkType.constrMark);
            mark.Create();
        }

        [CommandMethod("CK_KOTA_POZIOM")]
        public void ElevationMarkPlate()
        {
            IElevationMark mark = GetFactory(AppSettings.Instance.DrawingStandard).GetElevationMark(ElevationMarkType.planeMark);
            mark.Create();
        }

        private IElevationMarkFactory GetFactory(DrawingStandards standards)
        {
            IElevationMarkFactory factory;
            using (ILifetimeScope scope = DI.Container.BeginLifetimeScope())
            {
                switch (standards)
                {
                    case DrawingStandards.PN_B_01025:
                        factory = scope.Resolve<IElevationMarkFactoryPNB01025>();
                        break;
                    case DrawingStandards.CADKIT:
                        factory = scope.Resolve<IElevationMarkFactoryCADKit>();
                        break;
                    default:
                        throw new NotImplementedException($"Brak implemetacji standardu {standards.ToString()}");
                }
            }

            return factory;
        }
    }
}
