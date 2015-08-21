using System;
using NDesk.Options;
using Yarp.Plugins.Api;
using Yarp.Plugins;
using System.Linq;
using System.Collections.Generic;
using Yarp.Logging;

namespace Yarp
{
    class Program
    {
        private static readonly IPluginManager PluginManager = new DefaultPluginManager();
        private static readonly IRetrievedPasswordsLogger Logger = new ConsoleLogger(); //We'll let users choose the logger later

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to YARP (Yet Another Retriever of Passwords) - by Joakim Skoog");

            bool shouldShowHelp = false;
            //string loggerOption = "";
            string retrievers = "";

            var p = new OptionSet()
            {
                {"h|help", "Shows the valid parameters and how to use them", v => shouldShowHelp = v != null},
                //{"o|output=", "Print results to console or file. Valid arguments are 'console' or a filepath", v => loggerOption = v},
                {"r|retrievers=", "Which retrievers that should be used for retrieving passwords. Valid arguments: all", v => retrievers = v}
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

            Console.WriteLine("Retrieving passwords...");
            var retrievedPasswords = RetrievePasswords(retrievers);
           
            Logger.Log(retrievedPasswords);
        }

        private static IEnumerable<PasswordRetrievalResult> RetrievePasswords(string retrievers)
        {
            var plugins = RetrievePlugins(retrievers);
            var retrievedPasswords = new List<PasswordRetrievalResult>();

            foreach (var passwords in plugins.Select(plugin => plugin.Plugin.RetrievePasswords()))
            {
                retrievedPasswords.AddRange(passwords);
            }

            return retrievedPasswords;
        }

        private static IEnumerable<YarpPluginContainer> RetrievePlugins(string retrievers)
        {
            if (string.Equals("all", retrievers, StringComparison.InvariantCultureIgnoreCase))
            {
                return PluginManager.GetAllPlugins();
            }

            return Enumerable.Empty<YarpPluginContainer>(); //We only support the 'all' option at the moment.
        } 

        private static void PrintHelp(OptionSet p)
        {
            Console.WriteLine("Usage: ");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
