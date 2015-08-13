using System.Collections.Generic;
using Yarp.Plugins.Api;

namespace Yarp.IO
{
    public interface IRetrievedPasswordsLogger
    {
        void Log(IEnumerable<PasswordRetrievalResult> retrievedPasswords);
    }
}