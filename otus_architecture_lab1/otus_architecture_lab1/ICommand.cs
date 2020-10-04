using System;


namespace otus_architecture_lab1
{
    public interface ICommand
    {
        void SetResultCallback(Action<bool, object> callback);
        void Run();
    }
}
