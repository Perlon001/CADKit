using CADKit.Models;
using CADKitElevationMarks.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitElevationMarks.Services
{
    public abstract class IconServiceFactory : IIconServiceFactory
    {
        public abstract IIconService CreateService();
    }
}
