using System;
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

            var operationName = operation.Remove(0, 1);
            var isSuccesOperationParse = Enum.TryParse(operationName, true, out OperationsEnum operationEnum);
            if (operation[0] != '-' || !isSuccesOperationParse)
            {
                Console.WriteLine("Доступны следующие команнды -add -update -delete -get -getall");
                return;
            }

            var parser = new EmployeeParser(':');
            var isSuccesParsing = parser.TryParse(args, out var ea);
            if (!isSuccesParsing) return;

            if (!ea.IsValid(operationEnum))
                return;

            var em = new EmployeesManager();
            switch (operationEnum)
            {
                case OperationsEnum.Add:
                    var resultInsert = em.Insert(ea.FirstName, ea.LastName, (decimal)ea.SalaryPerHour);
                    Console.WriteLine(resultInsert == null ? "Сотрудник не добавлен" : $"Запись о сотруднике добавлена: \n\r {resultInsert.GetInfo()}");
                    break;

                case OperationsEnum.Get:
                    var foundEmployee = em.Get((int)ea.Id);
                    Console.WriteLine(foundEmployee == null ? $"Сотрудник с Id={ea.Id} не найден" : $"{foundEmployee.GetInfo()}");
                    break;

                case OperationsEnum.GetAll:
                    var resultGetall = em.Get();
                    if (resultGetall == null || !resultGetall.Any())
                        Console.WriteLine("Сотрудники не найдены");
                    else
                        resultGetall.ForEach(e => Console.WriteLine(e.GetInfo()));
                    break;

                case OperationsEnum.Update:
                    var resultUpdate = em.Update((int)ea.Id, ea.FirstName, ea.LastName, ea.SalaryPerHour);
                    Console.WriteLine(resultUpdate == null ? $"Сотрудник с Id={ea.Id} не найден" : $"Запись о сотруднике обновлена: \n\r {resultUpdate.GetInfo()}");
                    break;

                case OperationsEnum.Delete:
                    var resultDelete = em.Remove((int)ea.Id);
                    Console.WriteLine(resultDelete == null ? $"Сотрудник с Id={ea.Id} не найден" : $"Запись о сотруднике удалена: \n\r {resultDelete.GetInfo()}");
                    break;
            }
        }
    }
}
