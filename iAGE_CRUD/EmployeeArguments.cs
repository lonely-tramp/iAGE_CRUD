using System;
using System.Collections.Generic;

namespace iAGE_CRUD
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
}
