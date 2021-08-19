using System.Collections.Generic;

namespace iAGE_CRUD.Actions
{
    abstract class Action : IAction
    {
        protected Action(IEnumerable<string> args)
        {
            Args = args;
        }

        protected IEnumerable<string> Args;

        public abstract void Execute();

    }
}
