
using Autofac;
using CADKit;
using CADKitElevationMarks.Contracts;
using System;
using System.Collections.Generic;

namespace CADKitElevationMarks.Models
{
    public abstract class ElevationMarkFactory
    {
        protected IIconService iconService;

        public ElevationMarkFactory(IIconService _iconService)
        {
            iconService = _iconService;
        }

        public abstract IEnumerable<ElevationMarkItem> GetMarkTypeList();

    }
}
