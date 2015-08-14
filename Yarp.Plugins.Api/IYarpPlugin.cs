using System.Collections.Generic;

namespace Yarp.Plugins.Api
{
    public interface IYarpPlugin
    {
        void Activate();

        void Deactivate();

        IEnumerable<PasswordRetrievalResult> RetrievePasswords();
    }
}