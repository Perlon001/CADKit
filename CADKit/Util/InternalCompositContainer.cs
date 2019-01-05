using CADKit.Contract;
using CADKit.Contract.Services;
using CADKit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Util
{
    class InternalCompositContainer : CompositeContainer
    {
        public InternalCompositContainer(ICompositeService compositeService) : base(compositeService)
        {
            container = compositeService.LoadComposites();
        }
    }
}
