using CADKitElevationMarks.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZwSoft.ZwCAD.DatabaseServices;

namespace CADKitElevationMarks.Models
{
    public abstract class BaseElevationMark : IElevationMark
    {
        protected readonly ElevationValue value;
        protected readonly IElevationMarkConfig config;
        protected IEnumerable<Entity> entityList;

        public string Sign { get { return value.Sign; } }

        public string Value { get { return value.Value; } }

        public IEnumerable<Entity> EntityList 
        { 
            get 
            {
                return entityList;
            } 
        }

        protected abstract void CreateEntityList();

        public BaseElevationMark(ElevationValue _value, IElevationMarkConfig _config)
        {
            value = _value;
            config = _config;
            entityList = new List<Entity>();
            CreateEntityList();
        }
    }
}
