using CADKit.Contracts;
using CADKitBasic.Utils.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitBasic.Utils
{
    public class CADKitModuleContainerBuilder
    {
        // private CompositeContainer container;
        public string Name { get; }
        public ICollection<IComposite> Composites { get; }

        private CADKitModuleContainerBuilder()
        {
            //container = new InternalCompositContainer(new CompositService());
        }

        //public static ICollection<CompositeContainer> Build()
        //{
        //    CADKitModuleContainerBuilder builder = new CADKitModuleContainerBuilder();
        //    ICollection<CompositeContainer> result = new List<CompositeContainer>();
        //    // jazda po assembly w celu poszukiwania klas dziedziczacych po CompositeModule i wołanie metody Load(CADKitModuleContainerBuilder builder) 

        //    return result;
        //}

        public CADKitModuleBuilderFluentApi Module(string name)
        {
            CADKitModuleContainerBuilder builder = new CADKitModuleContainerBuilder();
            return new CADKitModuleBuilderFluentApi(builder);
        }

        private void RegisterModule(CompositeModule module)
        {

        }
    }

    namespace Fluent
    {
        public interface IRegister
        {
            IRegister Register(IComposite leaf);
        }


        public class CADKitModuleBuilderFluentApi : IRegister
        {
            private readonly CADKitModuleContainerBuilder builder;

            public CADKitModuleBuilderFluentApi(CADKitModuleContainerBuilder builder)
            {
                this.builder = builder;
            }

            public IRegister Register(IComposite leaf)
            {
                builder.Composites.Add(leaf);
                return this;
            }
        }
    }

}
