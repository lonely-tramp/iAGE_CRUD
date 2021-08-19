using System.Collections.Generic;
using iAGE_CRUD.Model;
using iAGE_CRUD.Parsers;
using iAGE_CRUD.Storage;

namespace iAGE_CRUD.Actions.Employee
{
    public abstract class EmployeeAction : Action
    {
        protected EmployeeAction(IEnumerable<string> args) : base(args)
        {
            Storage = new FileStorageManager();
        }

        protected FileStorageManager Storage;

        protected EmployeeArguments EmployeeArguments;

        private Dictionary<string, string> _arguments;

        public bool TryGetEmployee()
        {
            return TryParseArguments() && IsValidArguments(_arguments) && TryValidateValues(_arguments, out EmployeeArguments);
        }

        public abstract override void Execute();
        protected abstract bool IsValidArguments(Dictionary<string, string> arguments);

        public bool TryParseArguments()
        {
            var ap = new ArgumentsParser(':');
            return ap.TryParse(Args, out _arguments);
        }

        protected static bool TryValidateValues(Dictionary<string, string> validatedArguments, out EmployeeArguments employee)
        {
            return ArgumentsValidator.IsValidValues(validatedArguments, out employee);
        }

    }
}
