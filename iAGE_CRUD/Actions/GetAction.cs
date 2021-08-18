using System;
using System.Collections.Generic;
using iAGE_CRUD.Parsers;
using iAge_CRUD.Parsers;

namespace iAge_CRUD.Actions
{
    class GetAction : Action
    {
        public GetAction(string[] args) : base(args)
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
