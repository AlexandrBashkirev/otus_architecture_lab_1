using System.Collections.Generic;

namespace otus_architecture_lab1
{
    public interface ICommandExecutor
    {
        void Execute(ICommand cmd);
        void Execute(IEnumerable<ICommand> cmds);
    }
}
