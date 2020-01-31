using CADKit.Models;
using System;

namespace CADKit.Events
{
    public delegate void ChangeColorSchemeEventHandler(object sender, ChangeColorSchemeEventArgs arg);

    public class ChangeColorSchemeEventArgs : EventArgs
    {
        public InterfaceScheme scheme { private set; get; }

        public ChangeColorSchemeEventArgs(InterfaceScheme scheme)
        {
            this.scheme = scheme;
        }
    }
}
