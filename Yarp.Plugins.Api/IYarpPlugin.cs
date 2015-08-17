using System.Collections.Generic;

namespace Yarp.Plugins.Api
{
    /// <summary>
    /// This is the interface that all password retrievers should implement. It's the main component of the Yarp system.
    /// </summary>
    public interface IYarpPlugin
    {
        /// <summary>
        /// Activates the plugin, this is to prepare plugins so that they can allocate resources or pre-compute certain things.
        /// </summary>
        void Activate();

        /// <summary>
        /// Deactivates the plugin, this enables plugins to deallocate resources.
        /// </summary>
        void Deactivate();

        /// <summary>
        /// Retrieves all the available passwords that this plugin can retrieve.
        /// </summary>
        /// <returns>The available passwords</returns>
        IEnumerable<PasswordRetrievalResult> RetrievePasswords();
    }
}