using System;
using NDesk.Options;

namespace Yarp
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to YARP (Yet Another Retriever of Passwords) - by Joakim Skoog");

            bool shouldShowHelp = false;
            //string loggerOption = "";
            //string retrievers = "";

            var p = new OptionSet()
            {
                {"h|help", "Shows the valid parameters and how to use them", v => shouldShowHelp = v != null},
                //{"o|output=", "Print results to console or file. Valid arguments are 'console' or a filepath", v => loggerOption = v},
                //{"r|retrievers=", "Which retrievers that should be used for retrieving passwords. Valid arguments: all", v => retrievers = v}
            };

            try
            {
                p.Parse(args);
            }
            catch (OptionException)
            {
                Console.WriteLine("Could not parse arguments. Please try --help for more information.");
            }

            if (shouldShowHelp)
            {
                PrintHelp(p);
                return;
            }
        }

        private static void PrintHelp(OptionSet p)
        {
            Console.WriteLine("Usage: ");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
