using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Events
{
    // public delegate void ChangeMarkSuffixEventHandler(object sender, ChangeMarkSuffixEventArgs args);

    public class ChangeMarkSuffixEventArgs : EventArgs
    {
        public string Suffix { get; private set; }
        public ChangeMarkSuffixEventArgs(string _suffix)
        {
            Suffix = _suffix;
        }
    }
}
