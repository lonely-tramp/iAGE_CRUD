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

            var isSuccesParsing = EmployeeArgumentsParser.TryParseArguments(args, out var ea);
            if (!isSuccesParsing) return;

            var em = new EmployeesManager();
            switch (operation)
            {
                case "-add":
                    if (!ea.IsValidForAdd()) 
                        return;

                    var resultInsert = em.Insert(ea.FirstName, ea.LastName, (decimal)ea.SalaryPerHour);
                    Console.WriteLine(resultInsert == null ? "Сотрудник не добавлен" : $"Запись о сотруднике добавлена: \n\r {resultInsert.GetInfo()}");
                    break;
                case "-get":
                    if (!ea.IsValidForGetOrDelete())
                        return;

                    var foundEmployee = em.Get((int)ea.Id);
                    Console.WriteLine(foundEmployee == null ? $"Сотрудник с Id={ea.Id} не найден" : $"{foundEmployee.GetInfo()}");
                    break;
                case "-getall":
                    var resultGetall = em.Get();
                    if (resultGetall == null || !resultGetall.Any()) 
                        Console.WriteLine("Сотрудники не найдены");
                    else 
                        resultGetall.ForEach(e => Console.WriteLine(e.GetInfo()));
                    break;
                case "-update":
                    if (!ea.IsValidForUpdate())
                        return;

                    var resultUpdate = em.Update((int)ea.Id, ea.FirstName, ea.LastName, ea.SalaryPerHour);
                    Console.WriteLine(resultUpdate == null ? $"Сотрудник с Id={ea.Id} не найден" : $"Запись о сотруднике обновлена: \n\r {resultUpdate.GetInfo()}");
                    break;
               
                case "-delete":
                    if (!ea.IsValidForGetOrDelete())
                        return;

                    var resultDelete = em.Remove((int)ea.Id);
                    Console.WriteLine(resultDelete == null ? $"Сотрудник с Id={ea.Id} не найден" : $"Запись о сотруднике удалена: \n\r {resultDelete.GetInfo()}");
                    break;
                default:
                    Console.WriteLine("Доступны следующие команнды -add -update -delete -get -getall");
                    break;
            }
        }
    }
}
