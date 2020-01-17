using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKitConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test parse:");
            
            Console.WriteLine("{0}", Double.Parse("15.150".Replace(".",","), NumberStyles.Number, CultureInfo.CreateSpecificCulture("pl-PL")));
            Console.WriteLine("{0}", Double.Parse("-15.10".Replace(".", ","), NumberStyles.Number, CultureInfo.CreateSpecificCulture("pl-PL")));
            Console.WriteLine("{0}", Double.Parse("+15.150".Replace(".", ","), NumberStyles.Number, CultureInfo.CreateSpecificCulture("pl-PL")));

            Console.ReadKey();
        }
    }
}
