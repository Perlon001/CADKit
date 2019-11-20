using Autofac;
using CADKit;
using CADKit.Models;
using CADKit.Services;
using CADKitElevationMarks.Contracts;
using CADProxy;
using CADProxy.Runtime;
using System;

//using System.Reflection;

//#if ZwCAD
//using ZwSoft.ZwCAD.ApplicationServices;
//#endif

//#if AutoCAD
//using Autodesk.AutoCAD.ApplicationServices;
//#endif

namespace CADKitElevationMarks
{
    public class Commands
    {
        private void CreateElevationMark(ElevationMarkType _markType)
        {
            SystemVariables variables = SystemVariableService.GetSystemVariables();

            IElevationMarkFactory markFactory = GetFactory(AppSettings.Instance.DrawingStandard);
            try
            {
                markFactory.ElevationMark(_markType).Run();
            }
            catch (Exception ex)
            {
                ProxyCAD.Editor.WriteMessage(ex.Message);
            }
            finally
            {
                SystemVariableService.RestoreSystemVariables(variables);
            }
            //object acObject = Application.ZcadApplication;
            //acObject.GetType().InvokeMember("ZoomExtents", BindingFlags.InvokeMethod, null, acObject, null);
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

        [CommandMethod("CK_KOTA_ARCH")]
        public void ElevationMarkArch()
        {
            CreateElevationMark(ElevationMarkType.archMark);
        }

        [CommandMethod("CK_KOTA_KONSTR")]
        public void ElevationMarkConstr()
        {
            CreateElevationMark(ElevationMarkType.constrMark);
        }

        [CommandMethod("CK_KOTA_POZIOM")]
        public void ElevationMarkPlate()
        {
            CreateElevationMark(ElevationMarkType.planeMark);
        }
    }
}
