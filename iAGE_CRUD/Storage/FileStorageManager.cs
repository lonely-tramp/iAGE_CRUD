using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using iAge_CRUD.Model;
using Newtonsoft.Json;

namespace iAGE_CRUD.Storage
{
    public class FileStorageManager
    {
        public FileStorageManager()
        {
            LoadFromFile();
        }

        public List<Employee> ListOfEmployees;

        private readonly string _storageFileName = ConfigurationManager.AppSettings.Get("StorageFileName") ?? "ListOfEmployees.json";

        public int GetNextId()
        {
            if (ListOfEmployees.Count == 0) return 1;
            return ListOfEmployees.OrderByDescending(i => i.Id).First().Id + 1;
        }

        public Employee Insert(string firstName, string lastName, decimal salaryPerHour)
        {
            var id = GetNextId();
            var employee = new Employee(id, firstName, lastName, salaryPerHour);
            ListOfEmployees.Add(employee);
            SaveToFile();
            return (Employee)ListOfEmployees.SingleOrDefault(e => e.Id == id)?.Clone();
        }

        public Employee Update(int id, string firstName, string lastName, decimal? salaryPerHour)
        {
            var employeeToUpdate = ListOfEmployees.SingleOrDefault(e => e.Id == id);
            if (employeeToUpdate == null) return null;
            if (firstName != null) employeeToUpdate.FirstName = firstName;
            if (lastName != null) employeeToUpdate.LastName = lastName;
            if (salaryPerHour != null) employeeToUpdate.SalaryPerHour = (decimal)salaryPerHour;
            SaveToFile();
            return (Employee)ListOfEmployees.SingleOrDefault(e => e.Id == id)?.Clone();
        }

        public Employee Remove(int id)
        {
            var employeeToRemove = ListOfEmployees.SingleOrDefault(e => e.Id == id);
            if (employeeToRemove == null) return null;
            ListOfEmployees.Remove(employeeToRemove);
            SaveToFile();
            return (Employee)employeeToRemove.Clone();
        }

        public Employee Get(int id)
        {
            return (Employee)ListOfEmployees.SingleOrDefault(e => e.Id == id)?.Clone();
        }

        public List<Employee> Get()
        {
            return ListOfEmployees.Select(e => (Employee)e?.Clone()).ToList();
        }

        private void SaveToFile()
        {
            var json = JsonConvert.SerializeObject(ListOfEmployees);
            try
            {
                File.WriteAllText(_storageFileName, json);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка сохранения файла:\n{e.Message}\n");
            }
        }

        private void LoadFromFile()
        {
            try
            {
                if (!File.Exists(_storageFileName))
                    File.Create(_storageFileName).Dispose();

                var json = File.ReadAllText(_storageFileName);
                
                if (string.IsNullOrWhiteSpace(json))
                    ListOfEmployees = new List<Employee>();
                else
                    ListOfEmployees = JsonConvert.DeserializeObject<List<Employee>>(json);
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine($"Ошибка десериализации файла:\n{e.Message}\n");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка чтения файла:\n{e.Message}\n");
            }
        }



    }
}
