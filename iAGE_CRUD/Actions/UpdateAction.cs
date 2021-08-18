using System;
using System.Collections.Generic;
using iAGE_CRUD.Parsers;
using iAge_CRUD.Parsers;

namespace iAge_CRUD.Actions
{
    class UpdateAction : Action
    {
        public UpdateAction(string[] args) : base(args)
        {
        }

        private const ActionsEnum Action = ActionsEnum.Update;

        public override void Execute()
        {
            if (!TryGetEmployee())
                return;

            var result = Storage.Update(Convert.ToInt32(EmployeeArguments.Id), EmployeeArguments.FirstName, EmployeeArguments.LastName, Convert.ToDecimal(EmployeeArguments.SalaryPerHour));
            Console.WriteLine(result == null ? $"Сотрудник с Id={EmployeeArguments.Id} не найден" : $"Запись о сотруднике обновлена: \n\r {result}");
        }

        protected override bool IsValidArguments(Dictionary<string, string> arguments)
        {
            return ArgumentsValidator.IsValidArguments(arguments, Action);
        }
    }
}
