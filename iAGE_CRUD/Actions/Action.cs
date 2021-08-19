using System.Collections.Generic;
using iAge_CRUD.Actions;

namespace iAGE_CRUD.Actions
{
    public abstract class Action : IAction
    {
        protected Action(IEnumerable<string> args)
        {
            _args = args;
        }

        protected readonly IEnumerable<string> _args;

        public abstract void Execute();

    }
}
