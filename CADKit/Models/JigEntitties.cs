using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CADKit.Extensions;

#if ZwCAD
using ZwSoft.ZwCAD.EditorInput;
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.GraphicsInterface;
#endif

#if AutoCAD
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.GraphicsInterface;
#endif

namespace CADKit.Models
{
    public class JigEntitties : DrawJig
    {
        private IEnumerable<Entity> entities;
        private IEnumerable<Entity> buffer;

        public JigEntitties(IEnumerable<Entity> _entities)
        {
            buffer = _entities;
            entities = _entities.Clone();
        }

        protected override SamplerStatus Sampler(JigPrompts _prompts)
        {
            throw new NotImplementedException();
        }

        protected override bool WorldDraw(WorldDraw _draw)
        {
            throw new NotImplementedException();
        }
    }
}
