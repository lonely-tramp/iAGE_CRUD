using System.Collections.Generic;

namespace iAGE_CRUD.Actions
{
    public abstract class Action : IAction
    {
        protected Action(IEnumerable<string> args)
        {
            Args = args;
        }

        protected readonly IEnumerable<string> Args;

        public abstract void Execute();

    }
}
