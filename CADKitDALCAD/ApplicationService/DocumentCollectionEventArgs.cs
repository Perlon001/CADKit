using CADKit.ServiceCAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.ApplicationServices;

namespace CADKitDALCAD.ApplicationService
{
    public sealed class DocumentCollectionEventArgs : EventArgs
    {
        private dynamic documentCollectionEventArgs;

        public DocumentCollectionEventArgs()
        {
            switch (CADEnvironment.Instance.Platform)
            {
                case CADPlatforms.ZwCAD:
                    documentCollectionEventArgs = new DocumentCollectionEventArgs();
                    break;
                case CADPlatforms.AutoCAD:
                    throw new NotImplementedException("Brak implementacji platformy AutoCAD");
                    //break;
            }
        }
    }
}
