using System;
using System.Collections.Generic;
using iAGE_CRUD.Parsers;
using iAge_CRUD.Parsers;

namespace iAge_CRUD.Actions
{
    class AddAction : Action
    {
        public AddAction(string[] args) : base(args)
        {
        }

        private const ActionsEnum Action = ActionsEnum.Add;

        public override void Execute()
        {
            if (!TryGetEmployee())
                return;

            var result = Storage.Insert(EmployeeArguments.FirstName, EmployeeArguments.LastName, Convert.ToDecimal(EmployeeArguments.SalaryPerHour));
            Console.WriteLine(result == null ? "Сотрудник не добавлен" : $"Запись о сотруднике добавлена: \n\r {result}");
        }

        protected override bool IsValidArguments(Dictionary<string, string> arguments)
        {
            return ArgumentsValidator.IsValidArguments(arguments, Action);
        }
    }
}
