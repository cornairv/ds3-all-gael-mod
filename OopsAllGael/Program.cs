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
            //Test();

            Console.WriteLine("Press Enter...");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
            }

            return 0;
        }

        static void Test()
        {
            BND4 bnd = BND4.Read(@"chr\c1400.anibnd.dcx");
            foreach (BinderFile bndf in bnd.Files)
            {
                if (bndf.Name.EndsWith(".tae"))
                {
                    TAE3 tae = TAE3.Read(bndf.Bytes);
                    Console.WriteLine("ID: " + tae.ID);
                    Console.WriteLine("Skeleton: " + tae.SkeletonName);
                    foreach (TAE3.Animation anim in tae.Animations)
                    {
                        Console.WriteLine();
                        Console.WriteLine("File: " + anim.AnimFileName);
                        Console.WriteLine("ID: " + anim.ID);
                        foreach (TAE3.Event ev in anim.Events)
                        {
                            switch (ev)
                            {
                                case TAE3.Event.CreateSpEffect1 ev1:
                                    Console.WriteLine("\tType: SpEffect " + ev1.SpEffectID);
                                    break;
                                default:
                                    Console.WriteLine("\tType: " + ev.ToString());
                                    break;
                            }
                            Console.WriteLine("\tTime: " + ev.StartTime + " - " + ev.EndTime);
                        }
                        Console.WriteLine();
                        foreach (TAE3.EventGroup evg in anim.EventGroups)
                        {
                            Console.WriteLine("\tType: " + evg.Type);
                            foreach (int i in evg.Indices)
                            {
                                Console.WriteLine("\t\tIndex: " + i);
                            }
                        }

                    }
                }
            }
        }
    }
}
