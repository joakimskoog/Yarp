using System;

namespace Yarp.Plugins.Api
{
    public class PasswordRetrievalResult
    {
        public string Username { get; }
        public string Password { get; }
        public string Source { get; }
        public PasswordRetrievalStatus Status { get; }

        public bool WasSuccessful => Status == PasswordRetrievalStatus.Ok;

        public PasswordRetrievalResult(string username, string password, string source, PasswordRetrievalStatus status = PasswordRetrievalStatus.Ok)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (source == null) throw new ArgumentNullException(nameof(source));
            Username = username;
            Password = password;
            Source = source;
            Status = status;
        }
    }
}