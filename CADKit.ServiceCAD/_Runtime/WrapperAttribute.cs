using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADProxy.Runtime
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum, AllowMultiple = false, Inherited = true)]
    public sealed class WrapperAttribute : Attribute
    {
        private readonly ZwSoft.ZwCAD.Runtime.WrapperAttribute wrapper;
        public WrapperAttribute(string wrappedClass)
        {
            wrapper = new ZwSoft.ZwCAD.Runtime.WrapperAttribute(wrappedClass);
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
