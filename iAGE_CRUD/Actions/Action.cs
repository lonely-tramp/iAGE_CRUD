using System.Collections.Generic;
using iAGE_CRUD.Parsers;
using iAGE_CRUD.Storage;
using iAge_CRUD.Model;
using iAge_CRUD.Parsers;

namespace iAge_CRUD.Actions
{
    public abstract class Action : IAction
    {
        protected Action(string[] args)
        {
            _args = args;
            Storage = new FileStorageManager();
        }

        private readonly string[] _args;

        protected FileStorageManager Storage;

        protected EmployeeArguments EmployeeArguments;

        private Dictionary<string, string> _arguments;

        public bool TryGetEmployee()
        {
            return TryParseArguments() && IsValidArguments(_arguments) && TryValidateValues(_arguments, out EmployeeArguments);
        }

        public abstract void Execute();
        protected abstract bool IsValidArguments(Dictionary<string, string> arguments);

        public bool TryParseArguments()
        {
            var ap = new ArgumentsParser(':');
            return ap.TryParse(_args, out _arguments);
        }

        protected static bool TryValidateValues(Dictionary<string, string> validatedArguments, out EmployeeArguments employee)
        {
            return ArgumentsValidator.IsValidValues(validatedArguments, out employee);
        }

    }
}
