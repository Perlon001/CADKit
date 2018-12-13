using CADKitCore.Util;
using System;
using System.Reflection;
using ZwSoft.ZwCAD.ApplicationServices;
using ZwSoft.ZwCAD.Runtime;

namespace CADKitCore
{
    public class Autostart // : IExtensionApplication
    {
        public void Initialize()
        {
            // Create DI Container
            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\nStart with " + AppDomain.CurrentDomain.SetupInformation.ApplicationName);
            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("\nStart CADKitCore");
            try
            {
                DI.Container = Container.Builder.Build();
                Assembly.LoadFrom("D:\\dev\\CSharp\\Workspace\\CADKit\\CADKitElevationMarks\\bin\\Debug\\CADKitElevationMarks.dll");
            }
            catch (System.Exception ex)
            {
                Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Błąd: \n" + ex.Message);
            }

        }

        public void Terminate()
        {
        }
    }
}
