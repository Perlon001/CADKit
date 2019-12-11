using CADKitElevationMarks.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CADProxy;

#if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
using ZwSoft.ZwCAD.EditorInput;
#endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.EditorInput;
#endif

namespace CADKitElevationMarks.Models
{
    public abstract class ElevationMark : IElevationMark
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

        public ElevationMark(ElevationValue _value, IElevationMarkConfig _config)
        {
            this.value = _value;
            this.config = _config;
            entityList = new List<Entity>();
            CreateEntityList();
        }

        public PromptPointResult GetBasePoint()
        {
            PromptPointOptions promptPointOptions = new PromptPointOptions("Wskaż punkt wysokościowy:");
            PromptPointResult basePoint = ProxyCAD.Editor.GetPoint(promptPointOptions);

            return basePoint;
        }
    }
}
