using System;
using System.Collections.Generic;
using System.Threading;


namespace otus_architecture_lab1
{
    class ThreadCommandExecutor : ICommandExecutor
    {
        #region Variables

        private List<Thread> threads = new List<Thread>();
        private List<ICommand> commands = new List<ICommand>();

        private readonly object lockObj = new object();

        private bool running = true;

        #endregion



        #region Class lifecycle

        public ThreadCommandExecutor()
        {
            for(int i = 0; i < 4; i++)
            {
                Thread thread = new Thread(ThreadFunc);
                thread.Name = "ThreadFromPool"+i;
                thread.Start();

                threads.Add(thread);
            }
        }


        public void Dispose()
        {
            lock (lockObj)
            {
                commands.Clear();
                running = false;
            }

            foreach (var thread in threads)
            {
                if (thread.ThreadState == ThreadState.WaitSleepJoin)
                {
                    thread.Interrupt();
                }
            }
        }

        #endregion



        #region Methods

        public void Execute(ICommand cmd)
        {
            lock(lockObj)
            {
                commands.Add(cmd);
            }

            RunExecution();
        }


        public void Execute(IEnumerable<ICommand> cmds)
        {
            lock(lockObj)
            {
                commands.AddRange(cmds);
            }

            RunExecution();
        }


        private void RunExecution()
        {
            for (int i = 0; i < Math.Min(commands.Count, threads.Count); i++)
            {
                threads[i].Interrupt();
                SimpleServiceLocator.Instance.GetService<ILogger>()
                    .Log($"Tread runned {threads[i].Name}");
            }
        }


        private void ThreadFunc()
        {
            while (running)
            {
                try
                {
                    ICommand cmd = FetchCommandForRun();
                    if (cmd != null)
                    {
                        cmd.Run();
                    }
                    else
                    {
                        SimpleServiceLocator.Instance.GetService<ILogger>()
                            .Log($"Tread {Thread.CurrentThread.Name} go to sleep");
                        Thread.Sleep(Timeout.Infinite);
                    }
                }
                catch (ThreadInterruptedException e)
                {
                }
            }
        }


        private ICommand FetchCommandForRun()
        {
            ICommand cmd = null;

            lock (lockObj)
            {
                if (commands.Count > 0)
                {
                    cmd = commands[0];
                    commands.RemoveAt(0);
                }
            }

            return cmd;
        }

        #endregion
    }
}
