using System.Text;

namespace Yarp.IO
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class FileBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public abstract bool Exists(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public abstract string ReadAllText(string path);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public abstract string ReadAllText(string path, Encoding encoding);
    }
}