using System.Collections.Generic;

namespace Yarp.Plugins.Api
{
    public interface IYarpPlugin
    {
        IEnumerable<PasswordRetrievalResult> RetrievePasswords();
    }
}