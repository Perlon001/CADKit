using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Events
{
    public delegate void BeginCreateMarkEventHandler(object sender, BeginCreateMarkEventArgs e);
    public class BeginCreateMarkEventArgs : EventArgs
    {
        public int ID { private set; get; }
        public BeginCreateMarkEventArgs(int id)
        {
            ID = id;
        }
    }

}
