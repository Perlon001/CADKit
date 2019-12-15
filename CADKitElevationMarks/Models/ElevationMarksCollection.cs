using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Models
{
    public abstract class ElevationMarksCollection
    {
        ICollection<ElevationMark> markCollection;

        public abstract ICollection<ElevationMark> ColGetMarksCollection();
    }
}
