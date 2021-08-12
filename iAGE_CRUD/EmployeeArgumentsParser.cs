using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iAGE_CRUD
{
    public class EmployeeArgumentsParser
    {
        public struct EmployeeArguments
        {
            public int? Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public decimal? SalaryPerHour { get; set; }

            public bool IsValidForAdd()
            {
                var loosingArgs = new List<string>();

                if (string.IsNullOrWhiteSpace(FirstName)) loosingArgs.Add("FirstName");
                if (string.IsNullOrWhiteSpace(LastName)) loosingArgs.Add("LastName");
                if (SalaryPerHour == null) loosingArgs.Add("Salary");

                var result = loosingArgs.Count == 0;
                if (!result)
                    Console.WriteLine($"Не задан аргумент {String.Join(", ", loosingArgs.ToArray())}");
                return result;
            }
            public bool IsValidForUpdate()
            {
                var result = Id != null && !(string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(FirstName) && SalaryPerHour == null);
                if (!result)
                    Console.WriteLine("Id и минимум один из FirstName, LastName, Salary должны быть заданы");
                return result;
            }
            public bool IsValidForGetOrDelete()
            {
                var result = Id != null;
                if (!result)
                    Console.WriteLine("Id должен быть задан");
                return result;
            }
        }

        public static bool TryParseArguments(string[] args, out EmployeeArguments ea)
        {
            //var ea = new EmployeeArguments();
            ea = default;
            foreach (var arg in args)
            {
                char[] separators = { ':' };
                var maxCountToSplit = 2;
                var splittedArg = arg.Split(separators, maxCountToSplit);

                if (splittedArg.Length < 2)
                {
                    Console.WriteLine($"Неверный аргумент {arg}");
                    return false;
                }

                var name = splittedArg[0];
                var value = splittedArg[1];
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine($"Неверное значение аргумента {arg}");
                    return false;
                }

                switch (name)
                {
                    case "Id":
                        if (int.TryParse(value, out int resultId) && resultId > 0)
                            ea.Id = resultId;
                        else
                        {
                            Console.WriteLine($"Неверное значение аргумента {arg}");
                            return false;
                        }
                        break;
                    case "FirstName":
                        ea.FirstName = value;
                        break;
                    case "LastName":
                        ea.LastName = value;
                        break;
                    case "Salary":
                        if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var resultSalary))
                            ea.SalaryPerHour = resultSalary;
                        else
                        {
                            Console.WriteLine($"Неверное значение аргумента {arg}");
                            return false;
                        }
                        break;
                    default:
                        Console.WriteLine($"Неверное название аргумента {arg}");
                        break;
                }
            }
            return true;
        }
    }
}
