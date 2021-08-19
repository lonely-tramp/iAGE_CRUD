using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using iAGE_CRUD.Enums;
using iAGE_CRUD.Model;

namespace iAGE_CRUD.Parsers
{
    public static class ArgumentsValidator
    {
        public static bool IsValidArguments(Dictionary<string, string> arguments, ActionsEnum ae)
        {
            var isValid1 = true;
            var currentActionMask = ActionArgumetsEnum.None;
            foreach (var argument in arguments)
            {
                var isValidEmunParsing = Enum.TryParse(argument.Key, true, out ActionArgumetsEnum action);
                if (isValidEmunParsing)
                    currentActionMask |= action;
                else
                    isValid1 = false;
            }

            var isValid2 = ArgumentsInActions[ae].Contains(currentActionMask);
            var summaryIsvalid = isValid2 && isValid1;
            if (!summaryIsvalid)
            {
                Console.WriteLine("Недопустимые аргументы операции");
                Console.WriteLine("Допустимые аргументы:");
                var argsStr = string.Join(" или ", ArgumentsInActions[ae].Select(a => a.ToString()));
                Console.WriteLine($"Для {ae}: {argsStr}");
            }
            return summaryIsvalid;
        }

        public static bool IsValidValues(Dictionary<string, string> validatedArguments, out EmployeeArguments employee)
        {
            employee = new EmployeeArguments();
            var results = new List<bool>();
            foreach (var arg in validatedArguments)
            {
                var resultValue = true;
                var resultKey = true;
                switch (arg.Key)
                {
                    case "Id":
                        if (int.TryParse(arg.Value, out int resultId) && resultId > 0)
                            employee.Id = resultId;
                        else
                            resultValue = false;
                        break;
                    case "FirstName":
                        if (!string.IsNullOrWhiteSpace(arg.Value))
                            employee.FirstName = arg.Value;
                        else
                            resultValue = false;
                        break;
                    case "LastName":
                        if (!string.IsNullOrWhiteSpace(arg.Value))
                            employee.LastName = arg.Value;
                        else
                            resultValue = false;
                        break;
                    case "Salary":
                        if (decimal.TryParse(arg.Value, NumberStyles.Number, CultureInfo.InvariantCulture, out var resultSalary) && resultSalary > 0)
                            employee.SalaryPerHour = resultSalary;
                        else
                            resultValue = false;
                        break;
                    default:
                        resultKey = false;
                        break;
                }
                if (!resultKey)
                    Console.WriteLine($"Неверное название аргумента {arg.Key}");
                if (!resultValue)
                    Console.WriteLine($"Неверное значение \"{arg.Value}\" аргумента {arg.Key}");
                results.Add(resultValue && resultKey);
            }
            return results.All(r => r);
        }

        private static readonly Dictionary<ActionsEnum, List<ActionArgumetsEnum>> ArgumentsInActions = new Dictionary<ActionsEnum, List<ActionArgumetsEnum>>
        {
            [ActionsEnum.Add] = new List<ActionArgumetsEnum> { ActionArgumetsEnum.FirstName | ActionArgumetsEnum.LastName | ActionArgumetsEnum.Salary },
            [ActionsEnum.Get] = new List<ActionArgumetsEnum> { ActionArgumetsEnum.Id },
            [ActionsEnum.GetAll] = new List<ActionArgumetsEnum> { ActionArgumetsEnum.None },
            [ActionsEnum.Update] = new List<ActionArgumetsEnum>
            {
                ActionArgumetsEnum.Id | ActionArgumetsEnum.FirstName,
                ActionArgumetsEnum.Id | ActionArgumetsEnum.LastName,
                ActionArgumetsEnum.Id | ActionArgumetsEnum.Salary,
                ActionArgumetsEnum.Id | ActionArgumetsEnum.FirstName | ActionArgumetsEnum.LastName,
                ActionArgumetsEnum.Id | ActionArgumetsEnum.FirstName | ActionArgumetsEnum.Salary,
                ActionArgumetsEnum.Id | ActionArgumetsEnum.LastName | ActionArgumetsEnum.Salary,
                ActionArgumetsEnum.Id | ActionArgumetsEnum.FirstName | ActionArgumetsEnum.LastName | ActionArgumetsEnum.Salary,
            },
            [ActionsEnum.Delete] = new List<ActionArgumetsEnum> { ActionArgumetsEnum.Id }
        };
    }
}
