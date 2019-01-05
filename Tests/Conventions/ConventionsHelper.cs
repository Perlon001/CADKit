using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Tests.Conventions
{
    public static class ConventionsHelper
    {
        public static IEnumerable<Assembly> Assemblies(string _assemblyName)
        {
            yield return Assembly.Load(_assemblyName);
        }

        public static IEnumerable<Type> Types(string _assemblyName)
        {

           IEnumerable<Type> typ = null;

           var asm = Assemblies(_assemblyName);
            try
            {
                typ = asm.SelectMany(x => x.GetTypes());

            }
            catch
            {

            }
            return typ;
        }

        public static IEnumerable<Type> Classes(string _assemblyName)
        {
            return Types(_assemblyName)
                .Where(x => x.IsClass)
                .Where(x => !x.IsDefined(typeof(CompilerGeneratedAttribute), true));
        }

        public static IEnumerable<Type> Interfaces(string _assemblyName)
        {
            return Types(_assemblyName).Where(x => x.IsInterface);
        }
    }
}
