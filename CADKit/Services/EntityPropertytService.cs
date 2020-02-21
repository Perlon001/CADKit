using CADKit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.Services
{
    public class EntityPropertytService
    {
        private SortedSet<EntityComponent> components;
        public EntityPropertytService()
        {
            components = new SortedSet<EntityComponent>(Comparer<EntityComponent>.Create((x, y) => x.Title.CompareTo(y.Title)));
        }

        public void AddComponent(EntityComponent _component)
        {
            components.Add(_component);
        }
    }
}
