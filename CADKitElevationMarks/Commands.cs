using Autofac;
using CADKit.ServiceCAD;
using CADKitCore.Model;
using CADKitCore.Services;
using CADKitCore.Settings;
using CADKitCore.Util;
using CADKitDALCAD;
using CADKitElevationMarks.Contract;
using System;
using ZwSoft.ZwCAD.Runtime;

[assembly: CommandClass(typeof(CADKitElevationMarks.Commands))]

namespace CADKitElevationMarks
{
    public class Commands
    {
        [CommandMethod("CK_KOTA_ARCH")]
        public void ElevationMarkArch()
        {
            SystemVariables variables;
            variables = SystemVariableService.GetSystemVariables();

            IElevationMark mark = GetFactory(AppSettings.Instance.DrawingStandard).GetElevationMark(ElevationMarkType.archMark);
            try
            {
                mark.Create();
            }
            catch (System.Exception ex)
            {
                CADProxy.Editor.WriteMessage(ex.Message);
            }
            finally
            {
                SystemVariableService.RestoreSystemVariables(variables);
            }

            //object acObject = Application.ZcadApplication;
            //acObject.GetType().InvokeMember("ZoomExtents", BindingFlags.InvokeMethod, null, acObject, null);
        }

        [CommandMethod("CK_KOTA_KONSTR")]
        public void ElevationMarkConstr()
        {
            SystemVariables variables;
            variables = SystemVariableService.GetSystemVariables();

            IElevationMark mark = GetFactory(AppSettings.Instance.DrawingStandard).GetElevationMark(ElevationMarkType.constrMark);
            try
            {
                mark.Create();
            }
            catch (System.Exception ex)
            {
                CADProxy.Editor.WriteMessage(ex.Message);
            }
            finally
            {
                SystemVariableService.RestoreSystemVariables(variables);
            }
        }

        [CommandMethod("CK_KOTA_POZIOM")]
        public void ElevationMarkPlate()
        {
            SystemVariables variables;
            variables = SystemVariableService.GetSystemVariables();

            IElevationMark mark = GetFactory(AppSettings.Instance.DrawingStandard).GetElevationMark(ElevationMarkType.planeMark);
            try
            {
                mark.Create();
            }
            catch (System.Exception ex)
            {
                CADProxy.Editor.WriteMessage(ex.Message);
            }
            finally
            {
                SystemVariableService.RestoreSystemVariables(variables);
            }
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
