//using CADKit.Contracts;
//using CADKitBasic.Contracts.Services;
//using CADKitBasic.Models;
//using System.Collections.Generic;

//namespace CADKitBasic.Services
//{
//    public abstract class CompositeProvider : IComponentProvider
//    {
//        protected SortedSet<IComposite> composites = null;

//        protected CompositeProvider()
//        {
//            composites = new SortedSet<IComposite>(Comparer<IComposite>.Create((x,y) => x.Title.CompareTo(y.Title)));
//            Load();
//        }

//        public virtual SortedSet<IComposite> GetModules()
//        {
//            return composites;
//        }

//        public abstract void Load();
//    }
//}
