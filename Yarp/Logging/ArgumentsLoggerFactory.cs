using System;
using System.Collections.Generic;
using System.IO;
using Yarp.IO;
using Yarp.Plugins.Api;

namespace Yarp.Logging
{
    public class ArgumentsLoggerFactory : ILoggerFactory
    {
        private readonly FileBase _file;

        public ArgumentsLoggerFactory(FileBase file)
        {
            if (file == null) throw new ArgumentNullException("file");
            _file = file;
        }

        public IRetrievedPasswordsLogger CreateLogger(string logger)
        {
            if (string.IsNullOrEmpty(logger)) return new ConsoleLogger();

            if (string.Equals("console", logger, StringComparison.InvariantCultureIgnoreCase))
            {
                return new ConsoleLogger();
            }

            return new FileLogger(logger, _file);
        }
    }
}