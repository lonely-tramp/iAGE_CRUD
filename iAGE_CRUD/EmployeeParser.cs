using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace iAGE_CRUD
{
    public class EmployeeParser : OperationArgumentsParser
    {
        public EmployeeParser(char argumentOperationSeparator) : base(argumentOperationSeparator)
        {

        }

        public bool TryParse(string[] args, out EmployeeArguments ea)
        {
            ea = new EmployeeArguments();
            if (!base.TryParse(args, out var la)) 
                return false;

            var results = new List<bool>();
            foreach (var arg in la)
            {
                var resultValue = true;
                var resultKey = true;
                switch (arg.Key)
                {
                    case "Id":
                        if (int.TryParse(arg.Value, out int resultId))
                            ea.Id = resultId;
                        else
                            resultValue = false;
                        break;
                    case "FirstName":
                        if (!string.IsNullOrWhiteSpace(arg.Key))
                            ea.FirstName = arg.Value;
                        else
                            resultValue = false;
                        break;
                    case "LastName":
                        if (!string.IsNullOrWhiteSpace(arg.Key))
                            ea.LastName = arg.Value;
                        else
                            resultValue = false;
                        break;
                    case "Salary":
                        if (decimal.TryParse(arg.Value, NumberStyles.Number, CultureInfo.InvariantCulture, out var resultSalary))
                            ea.SalaryPerHour = resultSalary;
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
    }
}
