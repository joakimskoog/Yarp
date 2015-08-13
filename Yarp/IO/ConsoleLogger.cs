using System;
using System.Collections.Generic;
using Yarp.Plugins.Api;

namespace Yarp.IO
{
    public class ConsoleLogger : IRetrievedPasswordsLogger
    {
        private const string PasswordSeparator = "\n\n";
        private const string StatusLine = "Status: {0}";
        private const string UsernameLine = "Username: {0}";
        private const string PasswordLine = "Password: {0}";
        private const string SourceLine = "Source: {0}";

        public void Log(IEnumerable<PasswordRetrievalResult> retrievedPasswords)
        {
            foreach (var retrievedPass in retrievedPasswords)
            {
                Console.WriteLine(StatusLine, retrievedPass.Status);
                Console.WriteLine(UsernameLine, retrievedPass.Username);
                Console.WriteLine(PasswordLine, retrievedPass.Password);
                Console.WriteLine(SourceLine, retrievedPass.Source);
                Console.WriteLine(PasswordSeparator);
            }
        }
    }
}