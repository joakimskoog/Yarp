using System.IO;
using System.Text;

namespace Yarp.IO
{
    /// <summary>
    /// Wrapper for the <see cref="File"/> class.
    /// </summary>
    public class FileWrapper : FileBase
    {
        public override bool Exists(string path)
        {
            return File.Exists(path);
        }

        public override string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }

        public override string ReadAllText(string path, Encoding encoding)
        {
            return File.ReadAllText(path, encoding);
        }
    }
}