using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitDALCAD.ApplicationService
{
    public sealed class CommandEventArgs : EventArgs
    {
        private dynamic commandEventArgs;
        public CommandEventArgs()
        {
            switch(CADEnvironment.Instance.Platform)
            {
                case CADPlatforms.ZwCAD:
                    commandEventArgs = new ZwSoft.ZwCAD.ApplicationServices.CommandEventArgs();
                    break;
                case CADPlatforms.AutoCAD:
                    throw new NotImplementedException("Brak implementacji platformy AutoCAD");
                    //break;
            }
        }

        public string GlobalCommandName
        {
            get
            {
                return commandEventArgs.GlobalCommandName;
            }
        }
    }
}
