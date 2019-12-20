using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Contracts
{
    public interface IIconServiceFactory
    {
        IIconService CreateService();
    }
}
