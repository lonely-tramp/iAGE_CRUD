using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace iAGE_CRUD
{
    public class EmployeesManager
    {
        public EmployeesManager()
        {
            List = new List<Employee>();
            LoadFromFile();
        }

        private const string StorageFileName = "ListOfEmployees.json";

        private List<Employee> List { get; set; }

        private int GetNextId()
        {
            if (List.Count == 0) return 1;
            return List.OrderByDescending(i => i.Id).First().Id + 1;
        }

        private void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(List);
            File.WriteAllText(StorageFileName, json);
        }

        private void LoadFromFile()
        {
            if (!File.Exists(StorageFileName)) return;
            var json = File.ReadAllText(StorageFileName);
            List = JsonConvert.DeserializeObject<List<Employee>>(json);
        }

        public Employee Insert(string firstName, string lastName, decimal salaryPerHour)
        {
            LoadFromFile();
            var employee = new Employee(GetNextId(), firstName, lastName, salaryPerHour);
            List.Add(employee);
            SaveToFile();
            return employee;
        }

        public Employee Update(int id, string firstName, string lastName, decimal? salaryPerHour)
        {
            LoadFromFile();
            var employeeToUpdate = List.SingleOrDefault(e => e.Id == id);
            if (employeeToUpdate == null) return null;
            if (firstName != null) employeeToUpdate.FirstName = firstName;
            if (lastName != null) employeeToUpdate.LastName = lastName;
            if (salaryPerHour != null) employeeToUpdate.SalaryPerHour = (decimal)salaryPerHour;
            SaveToFile();
            return employeeToUpdate;
        }

        public List<Employee> Get()
        {
            LoadFromFile();
            return List;
        }

        public Employee Get(int id)
        {
            LoadFromFile();
            return List.SingleOrDefault(e => e.Id == id);
        }

        public Employee Remove(int id)
        {
            LoadFromFile();
            var employeeToRemove = List.SingleOrDefault(e => e.Id == id);
            if (employeeToRemove == null) return null;
            List.Remove(employeeToRemove);
            SaveToFile();
            return employeeToRemove;
        }
    }
}
