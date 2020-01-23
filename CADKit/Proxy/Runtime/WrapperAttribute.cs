using System;

#if ZwCAD
using CADRuntime = ZwSoft.ZwCAD.Runtime;
#endif

#if AutoCAD
using CADRuntime = Autodesk.AutoCAD.Runtime;
#endif

namespace CADKit.Runtime
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
    public sealed class WrapperAttribute : Attribute
    {
        private readonly CADRuntime.WrapperAttribute wrapper;
        public WrapperAttribute(string wrappedClass)
        {
            wrapper = new CADRuntime.WrapperAttribute(wrappedClass);
        }

        public string WrappedClass { 
            get 
            { 
                return wrapper.WrappedClass; 
            } 
            set 
            { 
                WrappedClass = wrapper.WrappedClass; 
            }
        }
    }

}
