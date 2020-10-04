using System;


namespace otus_architecture_lab1
{
    interface ILogger : IDisposable
    {
        void Log(string message);
    }
}
