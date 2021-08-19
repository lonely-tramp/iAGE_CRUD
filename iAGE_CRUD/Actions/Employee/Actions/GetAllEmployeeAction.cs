using System;
using System.Collections.Generic;
using System.Linq;
using iAGE_CRUD.Enums;
using iAGE_CRUD.Parsers;

namespace iAGE_CRUD.Actions.Employee.Actions
{
    class GetAllEmployeeAction : EmployeeAction
    {
        public GetAllEmployeeAction(IEnumerable<string> args) : base(args)
        {
        }

        private const ActionsEnum Action = ActionsEnum.GetAll;

        public override void Execute()
        {
            if (!TryGetEmployee())
                return;

            var result = Storage.Get();
            if (!result.Any())
                Console.WriteLine("Сотрудники не найдены");
            else
                result.ForEach(Console.WriteLine);
        }

        protected override bool IsValidArguments(Dictionary<string, string> arguments)
        {
            return ArgumentsValidator.IsValidArguments(arguments, Action);
        }

    }
}
