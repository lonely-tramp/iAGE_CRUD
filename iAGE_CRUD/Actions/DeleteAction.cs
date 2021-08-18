using System;
using System.Collections.Generic;
using iAGE_CRUD.Parsers;
using iAge_CRUD.Parsers;

namespace iAge_CRUD.Actions
{
    class DeleteAction : Action
    {
        public DeleteAction(string[] args) : base(args)
        {
        }

        private const ActionsEnum Action = ActionsEnum.Delete;

        public override void Execute()
        {
            if (!TryGetEmployee())
                return;

            var result = Storage.Remove(Convert.ToInt32(EmployeeArguments.Id));
            Console.WriteLine(result == null ? $"Сотрудник с Id={EmployeeArguments.Id} не найден" : $"Запись о сотруднике удалена: \n\r {result}");
        }

        protected override bool IsValidArguments(Dictionary<string, string> arguments)
        {
            return ArgumentsValidator.IsValidArguments(arguments, Action);
        }
    }
}
