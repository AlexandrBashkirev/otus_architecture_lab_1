using System;
using System.Threading.Tasks;


namespace otus_architecture_lab1
{
    abstract class AsyncCommand : CommandBase
    {
        #region Properties

        public bool IsDone
        {
            get;
            protected set;
        } = false;

        #endregion



        #region Methods

        public async Task Wait(int frequency = 25, int timeout = -1)
        {
            var waitTask = Task.Run(async () =>
            {
                while (!IsDone)
                {
                    await Task.Delay(frequency);
                }
            });

            if (waitTask != await Task.WhenAny(waitTask, Task.Delay(timeout)))
            {
                throw new TimeoutException();
            }
        }

        #endregion
    }
}
