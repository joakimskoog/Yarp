using System;
using System.Collections.Generic;
using Yarp.Plugins.Api;

namespace Yarp.IO
{
    public class FileLogger : IRetrievedPasswordsLogger
    {
        private readonly string _logPath;

        public FileLogger(string logPath)
        {
            if (logPath == null) throw new ArgumentNullException("logPath");
            _logPath = logPath;
        }

        public void Log(IEnumerable<PasswordRetrievalResult> retrievedPasswords)
        {
            //Log to file here
        }
    }
}