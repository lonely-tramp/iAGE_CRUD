using System;
using System.Collections.Generic;
using System.Linq;
using iAGE_CRUD;

namespace iAGE_CRUD
{
    public class EmployeeArguments
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal? SalaryPerHour { get; set; }

        private ArgumetsEnum GetMask()
        {
            var result = ArgumetsEnum.None;
            if (Id != null)
                result |= ArgumetsEnum.Id;
            if (!string.IsNullOrWhiteSpace(FirstName))
                result |= ArgumetsEnum.FirstName;
            if (!string.IsNullOrWhiteSpace(LastName))
                result |= ArgumetsEnum.LastName;
            if (SalaryPerHour != null)
                result |= ArgumetsEnum.Salary;
            return result;
        }

        private static readonly Dictionary<OperationsEnum, List<ArgumetsEnum>> _argumentsInOperations = new Dictionary<OperationsEnum, List<ArgumetsEnum>>
        {
            [OperationsEnum.Add] = new List<ArgumetsEnum> { ArgumetsEnum.FirstName | ArgumetsEnum.LastName | ArgumetsEnum.Salary },
            [OperationsEnum.Get] = new List<ArgumetsEnum> { ArgumetsEnum.Id },
            [OperationsEnum.GetAll] = new List<ArgumetsEnum> { ArgumetsEnum.None },
            [OperationsEnum.Update] = new List<ArgumetsEnum>
            {
                ArgumetsEnum.Id | ArgumetsEnum.FirstName,
                ArgumetsEnum.Id | ArgumetsEnum.LastName,
                ArgumetsEnum.Id | ArgumetsEnum.Salary,
                ArgumetsEnum.Id | ArgumetsEnum.FirstName | ArgumetsEnum.LastName,
                ArgumetsEnum.Id | ArgumetsEnum.FirstName | ArgumetsEnum.Salary,
                ArgumetsEnum.Id | ArgumetsEnum.LastName | ArgumetsEnum.Salary,
                ArgumetsEnum.Id | ArgumetsEnum.FirstName | ArgumetsEnum.LastName | ArgumetsEnum.Salary,
            },
            [OperationsEnum.Delete] = new List<ArgumetsEnum> { ArgumetsEnum.Id }
        };

        public bool IsValid(OperationsEnum oe)
        {
            var isValid = _argumentsInOperations[oe].Contains(GetMask());
            if (!isValid)
            {
                Console.WriteLine("Недопустимые аргументы операции");
                Console.WriteLine("Допустимые аргументы:");
                foreach (var op in _argumentsInOperations)
                {
                    var argsStr = string.Join(" или ", _argumentsInOperations[op.Key].Select(a => a.ToString()));
                    Console.WriteLine($"Для {op.Key}: {argsStr}");
                }
            }

            return isValid;
        }
    }
}
