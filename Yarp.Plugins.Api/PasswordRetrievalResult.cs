using System;

namespace Yarp.Plugins.Api
{
    public class PasswordRetrievalResult
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Source { get; private set; }
        public PasswordRetrievalStatus Status { get; private set; }

        public bool WasSuccessful { get { return Status == PasswordRetrievalStatus.Ok; } }

        public PasswordRetrievalResult(string username, string password, string source, PasswordRetrievalStatus status = PasswordRetrievalStatus.Ok)
        {
            if (username == null) throw new ArgumentNullException("username");
            if (password == null) throw new ArgumentNullException("password");
            if (source == null) throw new ArgumentNullException("source");
            Username = username;
            Password = password;
            Source = source;
            Status = status;
        }
    }
}