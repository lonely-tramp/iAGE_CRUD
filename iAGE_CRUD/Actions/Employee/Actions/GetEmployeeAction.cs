using System;
using System.Collections.Generic;
using iAge_CRUD.Actions;
using iAGE_CRUD.Parsers;

namespace iAGE_CRUD.Actions.Employee.Actions
{
    class GetEmployeeAction : EmployeeAction
    {
        public GetEmployeeAction(IEnumerable<string> args) : base(args)
        {
        }

        private const ActionsEnum Action = ActionsEnum.Get;

        public override void Execute()
        {
            if (!TryGetEmployee())
                return;

            var result = Storage.Get(Convert.ToInt32(EmployeeArguments.Id));
            Console.WriteLine(result == null ? "Сотрудник не найден" : $"{result}");
        }

        protected override bool IsValidArguments(Dictionary<string, string> arguments)
        {
            return ArgumentsValidator.IsValidArguments(arguments, Action);
        }
    }
}
