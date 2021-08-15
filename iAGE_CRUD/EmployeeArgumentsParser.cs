using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace iAGE_CRUD
{
    public class EmployeeArgumentsParser
    {
        public static bool TryParse(string[] args, out EmployeeArguments ea)
        {
            ea = default;
            var a = GetArguments(args);
            return TryParseValue(a, ref ea);
        }

        private static Dictionary<string, string> GetArguments(string[] args)
        {
            var arguments = new Dictionary<string, string>();
            foreach (var arg in args)
            {
                char[] separators = { ':' };
                const int maxCountToSplit = 2;
                var splittedArg = arg.Split(separators, maxCountToSplit);

                if (splittedArg.Length == 2)
                    arguments.Add(splittedArg[0], splittedArg[1]);
                else
                    Console.WriteLine($"Неверный формат аргумента {arg}\nДопустимый формат - key:value");
            }
            return arguments;
        }

        private static bool TryParseValue(Dictionary<string, string> args, ref EmployeeArguments ea)
        {
            var results = new List<bool>();
            foreach (var arg in args)
            {
                var resultValue = true;
                var resultKey = true;
                switch (arg.Key)
                {
                    case "Id":
                        if (int.TryParse(arg.Value, out int resultId) && resultId > 0)
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
