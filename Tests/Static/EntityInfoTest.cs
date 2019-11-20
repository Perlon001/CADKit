// #define ZwCAD

using CADKit.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;


// #if ZwCAD
using ZwSoft.ZwCAD.DatabaseServices;
using ZwSoft.ZwCAD.Geometry;
// #endif

#if AutoCAD
using Autodesk.AutoCAD.DatabaseServices;
#endif

namespace Tests.Static
{
    public class EntityInfoTest
    {
        [Fact]
        public void return_valid_text_area()
        {
            var test = "aaa";
            var text = new DBText();

            Assert.NotEmpty( test );
        }
    }
}
