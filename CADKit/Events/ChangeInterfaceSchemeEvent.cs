using CADKit.Contracts;
using CADKit.Models;
using System;

namespace CADKit.Events
{
    //public delegate void ChangeInterfaceSchemeEventHandler(object sender, ChangeInterfaceSchemeEventArgs arg);

    public class ChangeInterfaceSchemeEventArgs : EventArgs
    {
        public IInterfaceSchemeService Service { private set; get; }

        public ChangeInterfaceSchemeEventArgs(IInterfaceSchemeService _service)
        {
            Service = _service;
        }
    }
}
