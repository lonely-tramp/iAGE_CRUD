using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace iAGE_CRUD
{
    public class EmployeesManager
    {
        public EmployeesManager()
        {
            if (!LoadFromFile())
                List = new List<Employee>();
        }

        private readonly string _storageFileName = ConfigurationManager.AppSettings.Get("StorageFileName") ?? "ListOfEmployees.json";

        private List<Employee> List { get; set; }

        private int GetNextId()
        {
            if (List.Count == 0) return 1;
            return List.OrderByDescending(i => i.Id).First().Id + 1;
        }

        private bool SaveToFile()
        {
            var json = JsonConvert.SerializeObject(List);
            try
            {
                File.WriteAllText(_storageFileName, json);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка сохранения файла:\n{e.Message}\n");
                return false;
            }
        }

        private bool LoadFromFile()
        {
            if (!File.Exists(_storageFileName)) return false;
            try
            {
                var json = File.ReadAllText(_storageFileName);
                List = JsonConvert.DeserializeObject<List<Employee>>(json);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка загрузки файла:\n{e.Message}\n");
                return false;
            }
        }

        public Employee Insert(string firstName, string lastName, decimal salaryPerHour)
        {
            if (!LoadFromFile()) return null;
            var employee = new Employee(GetNextId(), firstName, lastName, salaryPerHour);
            List.Add(employee);
            return !SaveToFile() ? null : employee;
        }

        public Employee Update(int id, string firstName, string lastName, decimal? salaryPerHour)
        {
            if (!LoadFromFile()) return null;
            var employeeToUpdate = List.SingleOrDefault(e => e.Id == id);
            if (employeeToUpdate == null) return null;
            if (firstName != null) employeeToUpdate.FirstName = firstName;
            if (lastName != null) employeeToUpdate.LastName = lastName;
            if (salaryPerHour != null) employeeToUpdate.SalaryPerHour = (decimal)salaryPerHour;
            return !SaveToFile() ? null : employeeToUpdate;
        }

        public List<Employee> Get()
        {
            if (!LoadFromFile()) return null;
            return List;
        }

        public Employee Get(int id)
        {
            if (!LoadFromFile()) return null;
            return List.SingleOrDefault(e => e.Id == id);
        }

        public Employee Remove(int id)
        {
            if (!LoadFromFile()) return null;
            var employeeToRemove = List.SingleOrDefault(e => e.Id == id);
            if (employeeToRemove == null) return null;
            List.Remove(employeeToRemove);
            return !SaveToFile() ? null : employeeToRemove;
        }
    }
}
