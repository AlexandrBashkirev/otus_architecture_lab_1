using System;


namespace otus_architecture_lab1
{
    public interface ILogger : IDisposable
    {
        void Log(string message);
    }
}
