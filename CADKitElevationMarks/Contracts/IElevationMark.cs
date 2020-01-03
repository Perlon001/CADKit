using CADKitElevationMarks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Contracts
{
    public interface IElevationMark
    {
        void Create(EntitiesSet entitiesSet);
    }
}
