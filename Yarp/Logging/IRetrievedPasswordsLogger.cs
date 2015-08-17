using System.Collections.Generic;
using Yarp.Plugins.Api;

namespace Yarp.Logging
{
    public interface IRetrievedPasswordsLogger
    {
        void Log(IEnumerable<PasswordRetrievalResult> retrievedPasswords);
    }
}