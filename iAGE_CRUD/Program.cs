using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace iAGE_CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Доступны следующие команнды -add -update -delete -get -getall");
                return;
            }
            var operation = args[0];
            args = args.Where((val, i) => i != 0)
                       .ToArray();

            ParseArguments(args, out var inputId, out var inputFirstName, out var inputLastName, out var inputSalary);

            //обработка операций
            if ((operation == "-update" || operation == "-get" || operation == "-delete") && inputId == null)
            {
                Console.WriteLine($"Не задан аргумент Id");
                return;
            }

            var loosingArgs = new List<string>();
            var em = new EmployeesManager();
            switch (operation)
            {
                case "-add":
                    if (inputFirstName == null) loosingArgs.Add("FirstName");
                    if (inputLastName == null) loosingArgs.Add("LastName");
                    if (inputSalary == null) loosingArgs.Add("Salary");
                    if (loosingArgs.Any())
                    {
                        Console.WriteLine($"Не задан аргумент {String.Join(", ", loosingArgs.ToArray())}");
                        return;
                    }
                    var resultInsert = em.Insert(inputFirstName, inputLastName, (decimal)inputSalary);
                    Console.WriteLine(resultInsert == null ? "Сотрудник не добавлен" : $"Запись о сотруднике добавлена: \n\r {resultInsert.GetInfo()}");
                    break;
                case "-update":
                    if (inputFirstName == null && inputLastName == null && inputSalary == null)
                    {
                        Console.WriteLine("Как минимум одно поле должно быть изменено");
                    }
                    else
                    {
                        var resultUpdate = em.Update((int)inputId, inputFirstName, inputLastName, inputSalary);
                        Console.WriteLine(resultUpdate == null ? $"Сотрудник с Id={inputId} не найден" : $"Запись о сотруднике обновлена: \n\r {resultUpdate.GetInfo()}");
                    }
                    break;
                case "-get":
                    var foundEmployee = em.Get((int)inputId);
                    Console.WriteLine(foundEmployee == null ? $"Сотрудник с Id={inputId} не найден" : $"{foundEmployee.GetInfo()}");
                    break;
                case "-delete":
                    var resultDelete = em.Remove((int)inputId);
                    Console.WriteLine(resultDelete == null ? $"Сотрудник с Id={inputId} не найден" : $"Запись о сотруднике удалена: \n\r {resultDelete.GetInfo()}");
                    break;
                case "-getall":
                    var resultGetall = em.Get();
                    if (resultGetall == null) Console.WriteLine("Сотрудники не найдены");
                    else resultGetall.ForEach(e => Console.WriteLine(e.GetInfo()));
                    break;
                default:
                    Console.WriteLine("Доступны следующие команнды -add -update -delete -get -getall");
                    break;

            }
        }
        private static void ParseArguments(string[] args, out int? inputId, out string inputFirstName, out string inputLastName, out decimal? inputSalary)
        {
            inputId = null;
            inputFirstName = null;
            inputLastName = null;
            inputSalary = null;
            
            foreach (var arg in args)
            {
                var arrArg = arg.Split(':');
                if (arrArg.Length < 2)
                {
                    Console.WriteLine($"Неверный аргумент {arg}");
                    return;
                }

                var name = arrArg[0];
                var value = arrArg[1];
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine($"Неверное значение аргумента {arg}");
                    return;
                }

                switch (name)
                {
                    case "Id":
                        if (int.TryParse(value, out int resultId) && resultId > 0)
                            inputId = resultId;
                        else
                        {
                            Console.WriteLine($"Неверное значение аргумента {arg}");
                            return;
                        }
                        break;
                    case "FirstName":
                        inputFirstName = value;
                        break;
                    case "LastName":
                        inputLastName = value;
                        break;
                    case "Salary":
                        if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var resultSalary))
                            inputSalary = resultSalary;
                        else
                        {
                            Console.WriteLine($"Неверное значение аргумента {arg}");
                            return;
                        }
                        break;
                    default:
                        Console.WriteLine($"Неверный аргумент {arg}");
                        break;
                }
            }
        }
    }
}
