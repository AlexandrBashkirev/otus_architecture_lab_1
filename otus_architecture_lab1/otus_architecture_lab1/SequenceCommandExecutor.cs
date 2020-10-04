using System.Collections.Generic;


namespace otus_architecture_lab1
{
    public class SequenceCommandExecutor : ICommandExecutor
    {
        public void Dispose()
        {

        }


        public void Execute(ICommand cmd)
        {
            cmd.Run();
        }


        public void Execute(IEnumerable<ICommand> cmds)
        {
            foreach(var cmd in cmds)
            {
                cmd.Run();
            }
        }
    }
}
