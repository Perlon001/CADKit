using CADKit.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CADKit.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new TestDBContext();
            test.Test();
            PressEnterToExit();
        }

        private static void PressEnterToExit()
        {
            Console.WriteLine();
            Console.WriteLine("Press 'Enter' to exit.");
            Console.ReadLine();
        }

    }
}
