namespace Yarp.Logging
{
    public interface ILoggerFactory
    {
        IRetrievedPasswordsLogger CreateLogger(string logger);
    }
}