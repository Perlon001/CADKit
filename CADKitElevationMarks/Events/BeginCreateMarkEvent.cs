using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Events
{
    public delegate void BeginMarkCreateEventHandler(object sender, BeginMarkCreateEventArgs args);
    public class BeginMarkCreateEventArgs : EventArgs
    {
        public int ID { private set; get; }
        public BeginMarkCreateEventArgs(int id)
        {
            ID = id;
        }
    }

}
