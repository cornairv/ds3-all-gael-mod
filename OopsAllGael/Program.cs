using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulsFormats;

namespace OopsAllGael
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                Directory.SetCurrentDirectory(args[0]);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("The specified directory does not exist. {0}");
                return 1;
            }

            if (!File.Exists("DarkSoulsIII.exe"))
            {
                Console.WriteLine("The specified directory is not a Dark Souls 3 installation.");
                return 1;
            }

            Gaelifier.Gaelify();

            Console.WriteLine("Press Enter...");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
            }

            return 0;
        }
    }
}
