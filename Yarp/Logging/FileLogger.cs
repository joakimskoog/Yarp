using System;
using System.Collections.Generic;
using Yarp.IO;
using Yarp.Plugins.Api;

namespace Yarp.Logging
{
    public class FileLogger : IRetrievedPasswordsLogger
    {
        private readonly string _logPath;
        private readonly FileBase _file;

        public FileLogger(string logPath, FileBase file)
        {
            if (logPath == null) throw new ArgumentNullException("logPath");
            if (file == null) throw new ArgumentNullException("file");
            _logPath = logPath;
            _file = file;
        }

        public void Log(IEnumerable<PasswordRetrievalResult> retrievedPasswords)
        {
            //Log to file here
        }
    }
}